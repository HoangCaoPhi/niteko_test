namespace BL
{
    public class BaseBL
    {
        protected readonly IDatabaseServiceBL _databaseService;
        public BaseBL(IDatabaseServiceBL databaseService)
        {
            _databaseService = databaseService;
        }
    }
}
