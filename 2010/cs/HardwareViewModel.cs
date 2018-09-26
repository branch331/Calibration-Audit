using NationalInstruments.SystemConfiguration;
using System;

namespace NationalInstruments.Examples.CalibrationAudit
{
    class HardwareViewModel
    {
        public HardwareViewModel(HardwareResourceBase resource)
        {
            UserAlias = resource.UserAlias;
            NumberOfExperts = resource.Experts.Count;
            Expert0ResourceName = resource.Experts[0].ResourceName;
            Expert0ProgrammaticName = resource.Experts[0].ExpertProgrammaticName;

            ProductResource productResource = resource as ProductResource;

            if (productResource == null)
            {
                return;
            }

            //Use try catch blocks to detect System Configuration exception
            //If a value is not supported by the device (returns an exception) then "N/A" is returned
            try
            {
                Model = productResource.ProductName;
                SerialNumber = productResource.SerialNumber;

                if (productResource.SupportsInternalCalibration)
                {
                    ReturnInternalCalData(productResource);
                }
                else
                {
                    IntLastCalDate = "N/A";
                    IntLastCalTemp = "N/A";
                }

                if (productResource.SupportsExternalCalibration)
                {
                    CalibrationOverdue = false;

                    ReturnExternalCalData(productResource);

                    try
                    {
                        RecommendedNextCal = productResource.ExternalCalibrationDueDate.ToString("MM-dd-yyyy");

                        CalibrationOverdue = productResource.ExternalCalibrationDueDate < DateTime.Now;
                    }
                    catch
                    {
                        RecommendedNextCal = "N/A";
                    }

                }
                else
                {
                    ExtLastCalDate = "N/A";
                    ExtLastCalTemp = "N/A";
                    RecommendedNextCal = "N/A";
                }

                ReturnTemperatureData(productResource);

            }
            catch (SystemConfigurationException ex)
            {
                Error = ex.ErrorCode.ToString();
            }
        }

        public void ReturnInternalCalData(ProductResource productResource)
        {
            try
            {
                IntLastCalDate = productResource.InternalCalibrationDate.ToString("MM-dd-yyyy");
                IntLastCalTemp = productResource.InternalCalibrationTemperature.ToString("0.00");
            }
            catch
            {
                IntLastCalDate = "N/A";
                IntLastCalTemp = "N/A";
            }
        }

        public void ReturnExternalCalData(ProductResource productResource)
        {
            try
            {
                ExtLastCalDate = productResource.ExternalCalibrationDate.ToString("MM-dd-yyyy");
            }
            catch
            {
                ExtLastCalDate = "N/A";
            }

            try
            {
                ExtLastCalTemp = productResource.ExternalCalibrationTemperature.ToString("0.00");
            }
            catch
            {
                ExtLastCalTemp = "N/A";
            }
        }

        public void ReturnTemperatureData(ProductResource productResource)
        {
            try
            {
                TemperatureSensor[] sensors = productResource.QueryTemperatureSensors(SensorInfo.Reading);
                Temperature = sensors[0].Reading.ToString("0.00"); //Sensor 0 is the internal temperature
            }
            catch
            {
                Temperature = "N/A";
            }
        }

        public string UserAlias
        {
            get;
            private set;
        }

        public string Expert0ResourceName
        {
            get;
            private set;
        }

        public string Expert0ProgrammaticName
        {
            get;
            private set;
        }

        public string Model
        {
            get;
            private set;
        }

        public string SerialNumber
        {
            get;
            private set;
        }

        public string IntLastCalDate
        {
            get;
            private set;
        }

        public string IntLastCalTemp
        {
            get;
            private set;
        }

        public string ExtLastCalDate
        {
            get;
            private set;
        }

        public string ExtLastCalTemp
        {
            get;
            private set;
        }

        public string RecommendedNextCal
        {
            get;
            private set;
        }

        public string Temperature
        {
            get;
            private set;
        }

        public string Error
        {
            get;
            private set;
        }

        public bool CalibrationOverdue
        {
            get;
            private set;
        }

        public int NumberOfExperts
        {
            get;
            private set;
        }
    }
}
