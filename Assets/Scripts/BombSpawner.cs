public class BombSpawner : Spawner<Bomb>
{
    protected override void Awake()
    {
        base.Awake();

        SpawnerLocator.BombSpawner = this;
    }
}