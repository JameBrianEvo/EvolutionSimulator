using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FoodTraits
{
    public const int DIET = 0;
    public const int NUMDIET = 3;
    public const int DIET_CARNIVORE = 0 << 8;
    public const int DIET_HERBIVORE = 1 << 8;
    public const int DIET_OMNIVORE = 2 << 8;

    public const int RESISTANCES = 1;
    public const int NUMRESISTANCES = 2;
    public const int RESISTANCES_NONE = 0 << 8;
    public const int RESISTANCES_POISONS = 1 << 8;
}
