namespace clockworks.Methods
{
    public static partial class Functions
    {
        public static int SizeOf(string _param)
        {
            if(_param == "float")
            {
                return sizeof(float);
            }
            else if(_param == "double")
            {
                return sizeof(double);
            }
            else if(_param == "int")
            {
                return sizeof(int);
            }
            Console.WriteLine("Unknown Type");
            return 404;
        }
    }
}