namespace clockworks.Classes
{
    public class KeyboardRegister
    {
        public KeysRegister Key {get;set;} 
        public KeyboardRegister()
        {
            Key = new KeysRegister();
        }
    }
}