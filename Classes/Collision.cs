namespace clockworks.Classes
{
    public class Collision
    {
        public bool top {get;set;}
        public bool bottom {get;set;}
        public bool left {get;set;}
        public bool right {get;set;}
        public Collision()
        {
            top = false;
            bottom = false;
            left = false;
            bottom = false;
        }
    }
}