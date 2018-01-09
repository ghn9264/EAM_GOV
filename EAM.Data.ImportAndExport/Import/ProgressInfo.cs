using NPOI.SS.Formula;

namespace EAM.Data.ImportAndExport.Import
{
    public   class ProgressInfo
    {

        private int _totalAssetsNum ;
        private int _importedAssetsNum;
        private int _importedPercentVal;
       
        public   ProgressInfo()

    {
          TotalAssetsNum = 0;
          ImportedAssetsNum = 0;
          ImportedPercentVal = 100;
         
    
    }

        public int TotalAssetsNum
        {
            get
            {
                return _totalAssetsNum;
            }
            set
            {
                lock (this)
                {
                    _totalAssetsNum = value;
                }
              
            }
        }

        public int ImportedAssetsNum
        {
            get
            {
                
                return _importedAssetsNum;
            }
            set
            {
                lock (this)
                {
                    _importedAssetsNum = value;
                }

              
            }
        }

        public int ImportedPercentVal
        {
            get
            {
                return _importedPercentVal;
            }
            set
            {
                lock (this)
                {
                    _importedPercentVal = value;
                }
               
            }
        }



    }

}