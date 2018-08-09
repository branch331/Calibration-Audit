using NationalInstruments.SystemConfiguration;

namespace NationalInstruments.Examples.CalibrationAudit
{
    class HardwareViewModel
    {
        public HardwareViewModel(ProductResource product)
        { 
            UserAlias = product.UserAlias;
            NumberOfExperts = product.Experts.Count;
            Expert0ResourceName = product.Experts[0].ResourceName;
            Expert0ProgrammaticName = product.Experts[0].ExpertProgrammaticName;

            ProductResource productResource = product as ProductResource;

            if (productResource != null)
            {
                ProductName = product.ProductName;
                Model = product.ModelNameNumber.ToString();
                SerialNumber = product.SerialNumber;
                
                //Needs filter logic here for calibration support
                IntLastCalDate = productResource.InternalCalibrationDate.ToString();
                IntLastCalTemp = productResource.InternalCalibrationTemperature.ToString(); //Celsius
                ExtLastCalDate = productResource.ExternalCalibrationDate.ToString();
                ExtLastCalTemp = productResource.ExternalCalibrationTemperature.ToString();
                RecommendedNextCal = productResource.ExternalCalibrationDueDate.ToString();
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
