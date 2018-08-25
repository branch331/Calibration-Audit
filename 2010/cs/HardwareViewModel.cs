using NationalInstruments.SystemConfiguration;

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


            if (productResource != null)
            {
                try
                {
                    Model = productResource.ProductName;
                    SerialNumber = productResource.SerialNumber;

                    if (productResource.SupportsInternalCalibration)
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
                    else
                    {
                        IntLastCalDate = "N/A";
                        IntLastCalTemp = "N/A";
                    }

                    if (productResource.SupportsExternalCalibration)
                    {
                        try
                        {
                            ExtLastCalDate = productResource.ExternalCalibrationDate.ToString("MM-dd-yyyy");
                            ExtLastCalTemp = productResource.ExternalCalibrationTemperature.ToString("0.00");
                            RecommendedNextCal = productResource.ExternalCalibrationDueDate.ToString("MM-dd-yyyy");
                        }
                        catch
                        {
                            ExtLastCalDate = "N/A";
                            ExtLastCalTemp = "N/A";
                            RecommendedNextCal = "N/A";
                        }
                        try
                        {
                            if (System.DateTime.Compare(productResource.ExternalCalibrationDueDate, System.DateTime.Now) < 0)
                            {
                                CalibrationOverdue = true;
                            }
                            else
                            {
                                CalibrationOverdue = false;
                            }
                        }
                        catch
                        {
                            CalibrationOverdue = false;
                        }
                    }
                    else
                    {
                        ExtLastCalDate = "N/A";
                        ExtLastCalTemp = "N/A";
                        RecommendedNextCal = "N/A";
                    }
                    
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
                catch (SystemConfigurationException ex)
                {
                    Error = ex.ErrorCode.ToString();
                }
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
