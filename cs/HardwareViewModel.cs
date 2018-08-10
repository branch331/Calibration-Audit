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
                ProductName = productResource.ProductName;
                Model = productResource.ModelNameNumber.ToString();
                SerialNumber = productResource.SerialNumber;
                
                if (productResource.SupportsInternalCalibration)
                {
                    IntLastCalDate = productResource.InternalCalibrationDate.ToString();
                    IntLastCalTemp = productResource.InternalCalibrationTemperature.ToString(); //Celsius
                }

                if (productResource.SupportsExternalCalibration)
                {
                    ExtLastCalDate = productResource.ExternalCalibrationDate.ToString();
                    ExtLastCalTemp = productResource.ExternalCalibrationTemperature.ToString();
                    RecommendedNextCal = productResource.ExternalCalibrationDueDate.ToString(); 
                    //If recommendedNExtCal date is overdue, make field red
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

        public string ProductName
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
