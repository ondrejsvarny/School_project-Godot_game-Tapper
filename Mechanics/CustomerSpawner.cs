using Godot;

public partial class CustomerSpawner : Node
{
    [Export] public PackedScene CustomerScene { get; set; }
    [Export] public Godot.Collections.Array<Marker2D> SpawnPoints { get; set; }

    [Export] public float BaseSpawnTime { get; set; } = 3.0f;
    [Export] public float SpawnTimeVariance { get; set; } = 1.5f;
    
    [Export] public float MinSpawnTime { get; set; } = 0.6f; 
    [Export] public float TimeDecreasePerSpawn { get; set; } = 0.02f;

    private Timer _spawnTimer;
    private RandomNumberGenerator _rng = new();

    public override void _Ready()
    {
        _rng.Randomize();
        
        _spawnTimer = new Timer();
        _spawnTimer.OneShot = true;
        _spawnTimer.Timeout += OnSpawnTimerTimeout;
        AddChild(_spawnTimer);

 
        ScheduleNextSpawn();
    }

    private void ScheduleNextSpawn()
    {
        BaseSpawnTime = Mathf.Max(MinSpawnTime, BaseSpawnTime - TimeDecreasePerSpawn);
        float safeVariance = Mathf.Min(SpawnTimeVariance, BaseSpawnTime * 0.5f);
        
        float nextTime = BaseSpawnTime + _rng.RandfRange(-safeVariance, safeVariance);
        _spawnTimer.Start(nextTime);
    }

    private void OnSpawnTimerTimeout()
    {
        SpawnCustomer();
        ScheduleNextSpawn();
    }

    private void SpawnCustomer()
    {
        int randomBarIndex = _rng.RandiRange(0, SpawnPoints.Count - 1);
        Marker2D spawnPoint = SpawnPoints[randomBarIndex];
        
        Node2D customer = CustomerScene.Instantiate<Node2D>();
        customer.GlobalPosition = spawnPoint.GlobalPosition;
        customer.ZIndex = 3;
        GetTree().CurrentScene.AddChild(customer);
    }
}