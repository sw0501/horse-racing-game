using System.Collections.Generic;

public static class AnimalStats
{
    public struct Stats
    {
        public float minSpeed;
        public float maxSpeed;
        public float fallRiskMultiplier; // 1 = 기본, 1보다 크면 낙마 확률 증가, 작으면 감소
    }

    // 원본 Horse_Move 기본값(minSpeed 0.02 / maxSpeed 0.2 / 확률 1/50000) 대비 상대적인 종별 프로필
    private static readonly Dictionary<AnimalType, Stats> Profiles = new Dictionary<AnimalType, Stats>
    {
        { AnimalType.Horse,  new Stats { minSpeed = 0.02f, maxSpeed = 0.20f, fallRiskMultiplier = 1.0f } },
        { AnimalType.Bear,   new Stats { minSpeed = 0.02f, maxSpeed = 0.15f, fallRiskMultiplier = 0.5f } },
        { AnimalType.Boar,   new Stats { minSpeed = 0.03f, maxSpeed = 0.18f, fallRiskMultiplier = 1.2f } },
        { AnimalType.Deer,   new Stats { minSpeed = 0.05f, maxSpeed = 0.22f, fallRiskMultiplier = 1.3f } },
        { AnimalType.Fox,    new Stats { minSpeed = 0.04f, maxSpeed = 0.21f, fallRiskMultiplier = 1.1f } },
        { AnimalType.Wolf,   new Stats { minSpeed = 0.03f, maxSpeed = 0.19f, fallRiskMultiplier = 0.9f } },
        { AnimalType.Rabbit, new Stats { minSpeed = 0.06f, maxSpeed = 0.25f, fallRiskMultiplier = 1.6f } },
    };

    public static Stats Get(AnimalType type)
    {
        if (Profiles.TryGetValue(type, out Stats stats))
        {
            return stats;
        }

        return Profiles[AnimalType.Horse];
    }
}
