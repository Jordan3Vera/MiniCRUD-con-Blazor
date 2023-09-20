namespace PequeñoCRUD.Data
{
    public class SqlConfiguration
    {
        //Establezco la conexión

        private string stringConection;
        public string StringConection { get => stringConection; }

        public SqlConfiguration(string Conn) {
            stringConection = Conn;
        }
    }
}
