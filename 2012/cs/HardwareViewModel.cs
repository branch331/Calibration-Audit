using NationalInstruments.SystemConfiguration;
using System;

namespace NationalInstruments.Examples.CalibrationAudit
{
    class HardwareViewModel // Internal class
    {
        public HardwareViewModel(HardwareResourceBase resource)
        {
            UserAlias = resource.UserAlias;
            NumberOfExperts = resource.Experts.Count;
            ExpertResourceName = resource.Experts[0].ResourceName;
            ExpertProgrammaticName = resource.Experts[0].ExpertProgrammaticName;

            ProductResource productResource = resource as ProductResource;

            if (productResource == null)
            {
                return;
            }

            // Use try catch blocks to detect System Configuration exception.
            // If a value is not supported by the device (returns an exception) then "N/A" is returned.
            try
            {
                Model = productResource.ProductName;
                SerialNumber = productResource.SerialNumber;

                if (productResource.SupportsInternalCalibration)
                {
                    ShowInternalCalData(productResource);
                }
                else
                {
                    InternalLastCalDate = "N/A";
                    InternalLastCalTemp = "N/A";
                }

                if (productResource.SupportsExternalCalibration)
                {
                    CalibrationOverdue = false;
                    ShowExternalCalData(productResource);

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
                    ExternalLastCalDate = "N/A";
                    ExternalLastCalTemp = "N/A";
                    RecommendedNextCal = "N/A";
                }

                ShowTemperatureData(productResource);

            }
            catch (SystemConfigurationException ex)
            {
                Error = ex.ErrorCode.ToString();
            }
        }

        public void ShowInternalCalData(ProductResource productResource)
        {
            try
            {
                InternalLastCalDate = productResource.InternalCalibrationDate.ToString("MM-dd-yyyy");
                InternalLastCalTemp = productResource.InternalCalibrationTemperature.ToString("0.00");
            }
            catch 
            {
                InternalLastCalDate = "N/A";
                InternalLastCalTemp = "N/A";
            }
        }

        public void ShowExternalCalData(ProductResource productResource)
        {
            try
            {
                ExternalLastCalDate = productResource.ExternalCalibrationDate.ToString("MM-dd-yyyy");
            }
            catch
            {
                ExternalLastCalDate = "N/A";
            }

            try
            {
                ExternalLastCalTemp = productResource.ExternalCalibrationTemperature.ToString("0.00");
            }
            catch
            {
                ExternalLastCalTemp = "N/A";
            }
        }

        public void ShowTemperatureData(ProductResource productResource)
        {
            try
            {
                TemperatureSensor[] sensors = productResource.QueryTemperatureSensors(SensorInfo.Reading);
                Temperature = sensors[0].Reading.ToString("0.00"); // Sensor 0 is the internal temperature.
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

        public string ExpertResourceName
        {
            get;
            private set;
        }

        public string ExpertProgrammaticName
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

        public string InternalLastCalDate
        {
            get;
            private set;
        }

        public string InternalLastCalTemp
        {
            get;
            private set;
        }

        public string ExternalLastCalDate
        {
            get;
            private set;
        }

        public string ExternalLastCalTemp
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
