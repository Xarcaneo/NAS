namespace Sailing.Core
{
    public class Map
    {
        public Map()
        {
            Ship = new Ship();
            Wind = new Wind(90f, 20f);
        }

        public Ship Ship { get; }

        public Wind Wind { get; }

        public void Update(float deltaTime)
        {
            Ship.Update(deltaTime, Wind);
        }
    }
}
