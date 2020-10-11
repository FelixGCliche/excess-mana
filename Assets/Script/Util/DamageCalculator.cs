namespace Script.Util
{
    public static class DamageCalculator
    {
        private const float DAMAGE_MULTIPLIER = 2;

        public static float CalculateDamage(float damageAmount, Elements damageElement, Elements targetElement)
        {
            if (targetElement != Elements.NONE)
            {
                if (IsEffective(damageElement, targetElement))
                    damageAmount *= DAMAGE_MULTIPLIER;
                else if (IsIneffective(damageElement, targetElement))
                    damageAmount /= DAMAGE_MULTIPLIER;
            }
            return damageAmount;
        }

        private static bool IsEffective(Elements damageElement, Elements targetElement)
        {
            return damageElement == Elements.FIRE && targetElement == Elements.EARTH ||
                   damageElement == Elements.EARTH && targetElement == Elements.WIND ||
                   damageElement == Elements.WIND && targetElement == Elements.WATER ||
                   damageElement == Elements.WATER && targetElement == Elements.FIRE;
        }

        private static bool IsIneffective(Elements damageElement, Elements targetElement)
        {
            return damageElement == Elements.FIRE && targetElement == Elements.WATER ||
                   damageElement == Elements.EARTH && targetElement == Elements.FIRE ||
                   damageElement == Elements.WIND && targetElement == Elements.EARTH ||
                   damageElement == Elements.WATER && targetElement == Elements.WIND;
        }
        
    }
}