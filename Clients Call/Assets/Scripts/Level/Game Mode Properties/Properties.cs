public class Properties : System.Object {

    public float GetValue(LevelConfig.LevelDifficulty pDifficulty) {
        switch (pDifficulty) {
            case LevelConfig.LevelDifficulty.Easy:
                return (float) this.GetType().GetProperty("Easy").GetValue(this, null);
            case LevelConfig.LevelDifficulty.Normal:
                return (float) this.GetType().GetProperty("Normal").GetValue(this, null);
            case LevelConfig.LevelDifficulty.Hard:
                return (float) this.GetType().GetProperty("Hard").GetValue(this, null);
            default:
                return -1;
        }
    }

    public float GetTileSpeed(bool pDropSpeed) {
        if (pDropSpeed) {
            return (float) this.GetType().GetProperty("DropTileSpeed").GetValue(this, null);
        } else {
            return (float) this.GetType().GetProperty("RaiseTileSpeed").GetValue(this, null);
        }
    }
}
