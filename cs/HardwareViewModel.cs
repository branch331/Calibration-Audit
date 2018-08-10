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

            ProductResource productResource = resource as ProductResource; //Cast hardwareresourcebase to productresource

            if (productResource != null)
            {
                Model = productResource.ProductName;
                SerialNumber = productResource.SerialNumber;
                
                //IntLastCalDate = productResource.HardwareRevision.ToString(); //using prop in productResourceBase doesn't break it
                //IntLastCalDate = productResource.IsChassis.ToString(); //using prop in productResourceBase class is fine
                //IntLastCalDate = productResource.IsSimulated.ToString(); //using IsSimulated prop works just fine... so not a class issue
                
                //why doesn't this work:
                if (productResource.SupportsInternalCalibration)
                {
                    IntLastCalDate = productResource.InternalCalibrationDate.ToString();
                    IntLastCalTemp = productResource.InternalCalibrationTemperature.ToString(); //Celsius
                }
                else
                {
                    IntLastCalDate = "";
                } 
                
                //RecommendedNextCal = productResource.ExternalCalibrationDueDate.ToString();
                /*
                if (productResource.SupportsInternalCalibration)
                {
                    IntLastCalDate = productResource.InternalCalibrationDate.ToString();
                    IntLastCalTemp = productResource.InternalCalibrationTemperature.ToString(); //Celsius
                }
                else
                {
                    IntLastCalDate = "";
                    IntLastCalTemp = "";
                }
                 * */
                
                if (productResource.SupportsExternalCalibration)
                {
                    ExtLastCalDate = productResource.ExternalCalibrationDate.ToString();
                    ExtLastCalTemp = productResource.ExternalCalibrationTemperature.ToString();
                    RecommendedNextCal = productResource.ExternalCalibrationDueDate.ToString();
                    //If recommendedNExtCal date is overdue, make field red
                }
                else
                {
                    ExtLastCalDate = "";
                    ExtLastCalTemp = "";
                    RecommendedNextCal = "";
                }
                 
                //Temperature = productResource.Temperature
                /*
                Error
                */
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

        public int NumberOfExperts
        {
            get;
            private set;
        }
    }
}
