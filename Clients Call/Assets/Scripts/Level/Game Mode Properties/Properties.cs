public class Properties : System.Object {

    public int GetValue(LevelConfig.LevelDifficulty pDifficulty) {
        switch (pDifficulty) {
            case LevelConfig.LevelDifficulty.Easy:
                return (int) this.GetType().GetProperty("Easy").GetValue(this, null);
            case LevelConfig.LevelDifficulty.Normal:
                return (int) this.GetType().GetProperty("Normal").GetValue(this, null);
            case LevelConfig.LevelDifficulty.Hard:
                return (int) this.GetType().GetProperty("Hard").GetValue(this, null);
            default:
                return -1;
        }
    }
}
