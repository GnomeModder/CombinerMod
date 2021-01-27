using RoR2;
using R2API;
using R2API.Utils;
using UnityEngine;
using System;
using System.Collections.Generic;

namespace Gnome.CombinerMod
{
    public class ComboItems
    {
        //First here are the indexes for each item
        public ItemIndex CarnivorousSlug;
        public ItemIndex ImprovisedWeapon;
        public ItemIndex NapalmRounds;
        public ItemIndex AncestralNecklace;
        public ItemIndex BloodsuckingFruit;
        public ItemIndex Bootlegs;
        public ItemIndex CrimsonBison;
        public ItemIndex ScorchedEarth;
        public ItemIndex MustardGas;
        public ItemIndex CyberneticInfusion;
        public ItemIndex RosePlating;
        public ItemIndex TheManifesto;
        public ItemIndex BirdsOfAFeather;
        public ItemIndex TheSpark;
        public ItemIndex InvisiblityCloak;
        public ItemIndex TheCollectiveUnconscious;
        public ItemIndex CrownOfFrost;
        public ItemIndex LuckyKey;
        public ItemIndex TheHardStuff;
        public ItemIndex BioFuelCell;
        public ItemIndex CrownOfEmbers;
        public ItemIndex AnniversaryGift;
        public ItemIndex CrownOfThunder;
        public ItemIndex LoyalSoldiers;
        public ItemIndex OverloadingPerforator;
        public ItemIndex CornedBeef;

        // Here's the dictionary that contains all the items necessary for each combo
        public Dictionary<ItemIndex, Tuple<ItemIndex, ItemIndex>> comboRequirement = new Dictionary<ItemIndex,Tuple<ItemIndex, ItemIndex>>();
        
        // An array of the item indeces in order
        private ItemIndex[] ItemsArray = new ItemIndex[26];
        #region fillingArray
        private void fillArray()
        {
            ItemsArray[0] = CarnivorousSlug;
            ItemsArray[1] = ImprovisedWeapon;
            ItemsArray[2] = NapalmRounds;
            ItemsArray[3] = AncestralNecklace;
            ItemsArray[4] = BloodsuckingFruit;
            ItemsArray[5] = Bootlegs;
            ItemsArray[6] = CrimsonBison;
            ItemsArray[7] = ScorchedEarth;
            ItemsArray[8] = MustardGas;
            ItemsArray[9] = CyberneticInfusion;
            ItemsArray[10] = RosePlating;
            ItemsArray[11] = TheManifesto;
            ItemsArray[12] = BirdsOfAFeather;
            ItemsArray[13] = TheSpark;
            ItemsArray[14] = InvisiblityCloak;
            ItemsArray[15] = TheCollectiveUnconscious;
            ItemsArray[16] = CrownOfFrost;
            ItemsArray[17] = LuckyKey;
            ItemsArray[18] = TheHardStuff;
            ItemsArray[19] = BioFuelCell;
            ItemsArray[20] = CrownOfEmbers;
            ItemsArray[21] = AnniversaryGift;
            ItemsArray[22] = CrownOfThunder;
            ItemsArray[23] = LoyalSoldiers;
            ItemsArray[24] = OverloadingPerforator;
            ItemsArray[25] = CornedBeef;
        }
        #endregion

        //Here's the one method we call to add all of the items
        public void AddAllItems()
        {
            AddCarnivorousSlug();
            AddImprovisedWeapon();
            AddNapalmRounds();
            AddAncestralNecklace();
            AddBloodsuckingFruit();
            AddBootlegs();
            AddCrimsonBison();
            AddScorchedEarth();
            AddMustardGas();
            AddCyberneticInfusion();
            AddRosePlating();
            AddTheManifesto();
            AddBirdsOfAFeather();
            AddTheSpark();
            AddInvisibilityCloak();
            AddTheCollectiveUnconscious();
            AddCrownOfFrost();
            AddLuckyKey();
            AddTheHardStuff();
            AddBioFuelCell();
            AddCrownOfEmbers();
            AddAnniversaryGift();
            AddCrownOfThunder();
            AddLoyalSoldiers();
            AddOverloadingPerforator();
            AddCornedBeef();

            fillArray();
        }

        public ItemIndex GetRandomPossibleCombo(CharacterMaster master)
        {
            Inventory inventory = master.inventory;
            List<int> thenumbers = new List<int>();
            for (int i = 0; i < ItemsArray.Length; i++)
            {
                thenumbers.Add(i);
            }
            Shuffle<int>(thenumbers);
            int[] shufflednumbers = thenumbers.ToArray();
            for (int i = 0; i < shufflednumbers.Length; i++)
            {
                int index = shufflednumbers[i];
                ItemIndex item = ItemsArray[index];
                Tuple<ItemIndex, ItemIndex> requirements = comboRequirement[item];
                if (inventory.GetItemCount(requirements.Item1) > 0 && inventory.GetItemCount(requirements.Item2) > 0 && item!=ScorchedEarth && item!=TheCollectiveUnconscious)
                {
                    Chat.AddMessage("gave item " + index.ToString());
                    inventory.RemoveItem(requirements.Item1);
                    inventory.RemoveItem(requirements.Item2);
                    Vector3 position = master.GetBody().corePosition;
                    RoR2.ScrapperController.CreateItemTakenOrb(position + 5 * Vector3.forward, master.gameObject, requirements.Item1);
                    RoR2.ScrapperController.CreateItemTakenOrb(position + 5 * Vector3.right, master.gameObject, requirements.Item2);
                    return item;
                }
            }
            Chat.AddMessage("no possible combos");
            return 0;
        }

        #region helpers
        private static System.Random rng = new System.Random();

        public static void Shuffle<T>(List<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
        #endregion
        #region CarnivorousSlug
        private void AddCarnivorousSlug()
        {
            ItemDef itemdef = new ItemDef
            {
                //More on these later
                name = "CARNIVOROUSSLUG_NAME",
                nameToken = "CARNIVOROUSSLUG_NAME", //? Still needed if we are assigning name in the line above?
                pickupToken = "CARNIVOROUSSLUG_PICKUP",
                descriptionToken = "CARNIVOROUSSLUG_DESC",
                //loreToken = "CARNIVOROUSSLUG_LORE",
                tier = ItemTier.NoTier,
                //You can create your own icons and prefabs through assetbundles, but to keep this boilerplate brief, we'll be using question marks.
                pickupIconPath = "@Combiner:Assets/Combiner/texCarnivorousSlugIcon.png",
                pickupModelPath = "Prefabs/PickupModels/PickupMystery",
                //Can remove determines if a shrine of order, or a printer can take this item, generally true, except for NoTier items.
                canRemove = false,
                //Hidden means that there will be no pickup notification, and it won't appear in the inventory at the top of the screen. This is useful for certain noTier helper items, such as the DrizzlePlayerHelper.
                hidden = false
            };

            var displayRules = new ItemDisplayRuleDict(null);

            CarnivorousSlug = ItemAPI.Add(new CustomItem(itemdef, displayRules));
            Tuple<ItemIndex, ItemIndex> requirements = new Tuple<ItemIndex, ItemIndex>(ItemIndex.HealWhileSafe, ItemIndex.RegenOnKill);
            comboRequirement.Add(CarnivorousSlug, requirements);

            R2API.LanguageAPI.Add("CARNIVOROUSSLUG_NAME", "Carnivorous Slug");
            R2API.LanguageAPI.Add("CARNIVOROUSSLUG_PICKUP", "Reset damage timer on kill");
            R2API.LanguageAPI.Add("CARNIVOROUSSLUG_DESC", "Reset damage timer on <style=cIsDamage>kill</style>.");
            //R2API.LanguageAPI.Add("CARNIVOROUSSLUG_LORE", "");

            RoR2.GlobalEventManager.onCharacterDeathGlobal += SlugTimerReset;
        }

        private void SlugTimerReset(DamageReport damage)
        {
            //TODO: make this work
            CharacterBody charb = damage.attackerBody;
            if(charb && charb.inventory && charb.inventory.GetItemCount(CarnivorousSlug) > 0)
            {
                Reflection.SetFieldValue<float>(charb, "outOfDangerStopwatch", float.PositiveInfinity);
            }
        }
        #endregion
        #region ImprovisedWeapon
        private void AddImprovisedWeapon()
        {
            ItemDef itemdef = new ItemDef
            {
                name = "IMPROVISEDWEAPON_NAME",
                nameToken = "IMPROVISEDWEAPON_NAME",
                pickupToken = "IMPROVISEDWEAPON_PICKUP",
                descriptionToken = "IMPROVISEDWEAPON_DESC",
                //loreToken = "IMPROVISEDWEAPON_LORE",
                tier = ItemTier.NoTier,
                pickupIconPath = "@Combiner:Assets/Combiner/texImprovisedWeaponIcon.png",
                pickupModelPath = "Prefabs/PickupModels/PickupMystery",
                //Can remove determines if a shrine of order, or a printer can take this item, generally true, except for NoTier items.
                canRemove = false,
                //Hidden means that there will be no pickup notification, and it won't appear in the inventory at the top of the screen. This is useful for certain noTier helper items, such as the DrizzlePlayerHelper.
                hidden = false
            };

            var displayRules = new ItemDisplayRuleDict(null);

            ImprovisedWeapon = ItemAPI.Add(new CustomItem(itemdef, displayRules));
            Tuple<ItemIndex, ItemIndex> requirements = new Tuple<ItemIndex, ItemIndex>(ItemIndex.Crowbar, ItemIndex.BleedOnHit);
            comboRequirement.Add(ImprovisedWeapon, requirements);

            R2API.LanguageAPI.Add("IMPROVISEDWEAPON_NAME", "Improvised Weapon");
            R2API.LanguageAPI.Add("IMPROVISEDWEAPON_PICKUP", "Lascerate healthy enemies");
            R2API.LanguageAPI.Add("IMPROVISEDWEAPON_DESC", "Apply 1 bleed <style=cStack>(+1 per stack)</style> per 500% damage dealt to enemies above 90% health");
            //R2API.LanguageAPI.Add("IMPROVISEDWEAPON_LORE", "");

            RoR2.GlobalEventManager.onServerDamageDealt += Lascertate;
        }

        private void Lascertate(DamageReport damageReport)
        {
            if (damageReport.attackerBody && damageReport.attackerBody.inventory && damageReport.combinedHealthBeforeDamage >= (0.9f * damageReport.victimBody.maxHealth))
            {
                CharacterBody charb = damageReport.attackerBody;
                int IWcount = charb.inventory.GetItemCount(ImprovisedWeapon);
                float stacksToApply = IWcount * (damageReport.damageDealt / (5f * charb.damage));
                for (int i = 0; i < stacksToApply; i++)
                {
                    DotController.InflictDot(damageReport.victim.gameObject, damageReport.attacker, DotController.DotIndex.Bleed, 3f * damageReport.damageInfo.procCoefficient, 1f);
                }
            }
        }
        #endregion
        #region NapalmRounds
        private void AddNapalmRounds()
        {
            ItemDef itemdef = new ItemDef
            {
                name = "NAPALMROUNDS_NAME",
                nameToken = "NAPALMROUNDS_NAME",
                pickupToken = "NAPALMROUNDS_PICKUP",
                descriptionToken = "NAPALMROUNDS_DESC",
                //loreToken = "NAPALMROUNDS_LORE",
                tier = ItemTier.NoTier,
                pickupIconPath = "@Combiner:Assets/Combiner/texNapalmRoundsIcon.png",
                pickupModelPath = "Prefabs/PickupModels/PickupMystery",
                //Can remove determines if a shrine of order, or a printer can take this item, generally true, except for NoTier items.
                canRemove = false,
                //Hidden means that there will be no pickup notification, and it won't appear in the inventory at the top of the screen. This is useful for certain noTier helper items, such as the DrizzlePlayerHelper.
                hidden = false
            };

            var displayRules = new ItemDisplayRuleDict(null);

            NapalmRounds = ItemAPI.Add(new CustomItem(itemdef, displayRules));
            Tuple<ItemIndex, ItemIndex> requirements = new Tuple<ItemIndex, ItemIndex>(ItemIndex.IgniteOnKill, ItemIndex.StickyBomb);
            comboRequirement.Add(NapalmRounds, requirements);

            R2API.LanguageAPI.Add("NAPALMROUNDS_NAME", "Napalm Rounds");
            R2API.LanguageAPI.Add("NAPALMROUNDS_PICKUP", "Chance to ignite on hit");
            R2API.LanguageAPI.Add("NAPALMROUNDS_DESC", "10% chance to ignite enemies on hit, setting all enemies within 12m <style=cStack>(+4m per stack)</style> on fire for 150% <style=cStack>(+75% per stack)</style> base damage.");
            //R2API.LanguageAPI.Add("NAPALMROUNDS_LORE", "");

            RoR2.GlobalEventManager.onServerDamageDealt += IgniteOnKillOnHit;
        }

        private void IgniteOnKillOnHit(DamageReport damage)
        {
            if (damage.attackerMaster && damage.attackerBody && damage.attackerBody.inventory)
            {
                int nrCount = damage.attackerBody.inventory.GetItemCount(NapalmRounds);
                if (nrCount > 0 && damage.damageInfo.procCoefficient > 0)
                {
                    float chanceToProc = 10 * damage.damageInfo.procCoefficient;
                    bool ignite = Util.CheckRoll(chanceToProc, damage.attackerMaster);
                    if (ignite)
                    {
                        ProcNapalm(damage, nrCount, damage.victimBody, damage.attackerTeamIndex);
                    }
                }
            }
        }

        private static List<HurtBox> hurtBoxBuffer = new List<HurtBox>();
        private static SphereSearch sphereSearch = new SphereSearch();
        private static GameObject ExplosionEffectPrefab = Resources.Load<GameObject>("Prefabs/Effects/ImpactEffects/IgniteExplosionVFX");
        private static void ProcNapalm(DamageReport damageReport, int igniteOnKillCount, CharacterBody victimBody, TeamIndex attackerTeamIndex)
        {
            float num = 8f + 4f * (float)igniteOnKillCount;
            float radius = victimBody.radius;
            float num2 = num + radius;
            Vector3 corePosition = victimBody.corePosition;
            sphereSearch.origin = corePosition;
            sphereSearch.mask = LayerIndex.entityPrecise.mask;
            sphereSearch.radius = num2;
            sphereSearch.RefreshCandidates();
            sphereSearch.FilterCandidatesByHurtBoxTeam(TeamMask.GetUnprotectedTeams(attackerTeamIndex));
            sphereSearch.FilterCandidatesByDistinctHurtBoxEntities();
            sphereSearch.OrderCandidatesByDistance();
            sphereSearch.GetHurtBoxes(hurtBoxBuffer);
            sphereSearch.ClearCandidates();
            for (int i = 0; i < hurtBoxBuffer.Count; i++)
            {
                HurtBox hurtBox = hurtBoxBuffer[i];
                if (hurtBox.healthComponent)
                {
                    DotController.InflictDot(hurtBox.healthComponent.gameObject, damageReport.attacker, DotController.DotIndex.Burn, 1.5f + 1.5f * (float)igniteOnKillCount, 1f);
                }
            }
            hurtBoxBuffer.Clear();
            EffectManager.SpawnEffect(ExplosionEffectPrefab, new EffectData
            {
                origin = corePosition,
                scale = num2,
                rotation = Util.QuaternionSafeLookRotation(damageReport.damageInfo.force)
            }, true);
        }
        #endregion
        #region AncestralNecklace
        private void AddAncestralNecklace()
        {
            ItemDef itemdef = new ItemDef
            {
                name = "ANCESTRALNECKLACE_NAME",
                nameToken = "ANCESTRALNECKLACE_NAME",
                pickupToken = "ANCESTRALNECKLACE_PICKUP",
                descriptionToken = "ANCESTRALNECKLACE_DESC",
                //loreToken = "ANCESTRALNECKLACE_LORE",
                tier = ItemTier.NoTier,
                pickupIconPath = "@Combiner:Assets/Combiner/texAncestralNecklaceIcon.png",
                pickupModelPath = "Prefabs/PickupModels/PickupMystery",
                //Can remove determines if a shrine of order, or a printer can take this item, generally true, except for NoTier items.
                canRemove = false,
                //Hidden means that there will be no pickup notification, and it won't appear in the inventory at the top of the screen. This is useful for certain noTier helper items, such as the DrizzlePlayerHelper.
                hidden = false
            };

            var displayRules = new ItemDisplayRuleDict(null);

            AncestralNecklace = ItemAPI.Add(new CustomItem(itemdef, displayRules));
            Tuple<ItemIndex, ItemIndex> requirements = new Tuple<ItemIndex, ItemIndex>(ItemIndex.BarrierOnKill, ItemIndex.Tooth);
            comboRequirement.Add(AncestralNecklace, requirements);

            R2API.LanguageAPI.Add("ANCESTRALNECKLACE_NAME", "Ancestral Necklace");
            R2API.LanguageAPI.Add("ANCESTRALNECKLACE_PICKUP", "Heal on Kills");
            R2API.LanguageAPI.Add("ANCESTRALNECKLACE_DESC", "Heal for 8hp <style=cStack>(+8 per stack)</style> instantly on kill.");
            //R2API.LanguageAPI.Add("ANCESTRALNECKLACE_LORE", "");

            RoR2.GlobalEventManager.onCharacterDeathGlobal += NecklaceHeal;
        }

        private void NecklaceHeal(DamageReport damageReport)
        {
            if (damageReport.attackerBody && damageReport.attackerBody.inventory)
            {
                int ANcount = damageReport.attackerBody.inventory.GetItemCount(AncestralNecklace);
                if (ANcount > 0)
                {
                    //TODO: add fx
                    damageReport.attackerBody.healthComponent.Heal(8 * ANcount, damageReport.damageInfo.procChainMask);
                }
            }
        }
        #endregion
        #region BloodsuckingFruit
        private void AddBloodsuckingFruit()
        {
            ItemDef itemdef = new ItemDef
            {
                name = "BLOODSUCKINGFRUIT_NAME",
                nameToken = "BLOODSUCKINGFRUIT_NAME",
                pickupToken = "BLOODSUCKINGFRUIT_PICKUP",
                descriptionToken = "BLOODSUCKINGFRUIT_DESC",
                //loreToken = "BLOODSUCKINGFRUIT_LORE",
                tier = ItemTier.NoTier,
                pickupIconPath = "Textures/MiscIcons/texMysteryIcon",
                pickupModelPath = "Prefabs/PickupModels/PickupMystery",
                //Can remove determines if a shrine of order, or a printer can take this item, generally true, except for NoTier items.
                canRemove = false,
                //Hidden means that there will be no pickup notification, and it won't appear in the inventory at the top of the screen. This is useful for certain noTier helper items, such as the DrizzlePlayerHelper.
                hidden = false
            };

            var displayRules = new ItemDisplayRuleDict(null);

            BloodsuckingFruit = ItemAPI.Add(new CustomItem(itemdef, displayRules));
            Tuple<ItemIndex, ItemIndex> requirements = new Tuple<ItemIndex, ItemIndex>(ItemIndex.Seed, ItemIndex.BossDamageBonus);
            comboRequirement.Add(BloodsuckingFruit, requirements);

            R2API.LanguageAPI.Add("BLOODSUCKINGFRUIT_NAME", "Bloodsucking Fruit");
            R2API.LanguageAPI.Add("BLOODSUCKINGFRUIT_PICKUP", "Heal for 10hp each hit done to a boss");
            R2API.LanguageAPI.Add("BLOODSUCKINGFRUIT_DESC", "Heal for 10hp <style=cStack>(+10 per stack)</style> instantly each time you damage a boss.");
            //R2API.LanguageAPI.Add("BLOODSUCKINGFRUIT_LORE", "");

            RoR2.GlobalEventManager.onServerDamageDealt += BSFruitHeal;
        }

        private void BSFruitHeal(DamageReport damageReport)
        {
            if (damageReport.attackerBody && damageReport.attackerBody.inventory)
            {
                int BSFcount = damageReport.attackerBody.inventory.GetItemCount(BloodsuckingFruit);
                if (BSFcount > 0 && damageReport.victimIsBoss)
                {
                    damageReport.attackerBody.healthComponent.Heal(7 * BSFcount * damageReport.damageInfo.procCoefficient, damageReport.damageInfo.procChainMask);
                }
            }
        }
        #endregion
        #region Bootlegs
        private void AddBootlegs()
        {
            ItemDef itemdef = new ItemDef
            {
                name = "BOOTLEGS_NAME",
                nameToken = "BOOTLEGS_NAME",
                pickupToken = "BOOTLEGS_PICKUP",
                descriptionToken = "BOOTLEGS_DESC",
                //loreToken = "BOOTLEGS_LORE",
                tier = ItemTier.NoTier,
                pickupIconPath = "Textures/MiscIcons/texMysteryIcon",
                pickupModelPath = "Prefabs/PickupModels/PickupMystery",
                //Can remove determines if a shrine of order, or a printer can take this item, generally true, except for NoTier items.
                canRemove = false,
                //Hidden means that there will be no pickup notification, and it won't appear in the inventory at the top of the screen. This is useful for certain noTier helper items, such as the DrizzlePlayerHelper.
                hidden = false
            };

            var displayRules = new ItemDisplayRuleDict(null);

            Bootlegs = ItemAPI.Add(new CustomItem(itemdef, displayRules));
            Tuple<ItemIndex, ItemIndex> requirements = new Tuple<ItemIndex, ItemIndex>(ItemIndex.Firework, ItemIndex.Missile);
            comboRequirement.Add(Bootlegs, requirements);

            R2API.LanguageAPI.Add("BOOTLEGS_NAME", "Bootlegs");
            R2API.LanguageAPI.Add("BOOTLEGS_PICKUP", "Chance to proc fireworks on hit");
            R2API.LanguageAPI.Add("BOOTLEGS_DESC", "10% chance to proc 8 <style=cStack>(+4 per stack)</style> fireworks on hit.");
            //R2API.LanguageAPI.Add("BOOTLEGS_LORE", "");

            RoR2.GlobalEventManager.onServerDamageDealt += Blproc;
        }

        private void Blproc(DamageReport damageReport)
        {
            if (damageReport.attackerBody && damageReport.attackerBody.inventory)
            {
                int Blcount = damageReport.attackerBody.inventory.GetItemCount(Bootlegs);
                if (Blcount > 0 && damageReport.damageInfo.procCoefficient > 0)
                {
                    float chanceToProc = 10 * damageReport.damageInfo.procCoefficient;
                    bool firework = Util.CheckRoll(chanceToProc, damageReport.attackerMaster);
                    if (firework)
                    {
                        Vector3 position = damageReport.victimBody.transform.position + ((damageReport.victimBody.radius + 3)* Vector3.up);
                        FireworkLauncher component4 = UnityEngine.Object.Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/FireworkLauncher"), position, Quaternion.identity).GetComponent<FireworkLauncher>();
                        component4.owner = damageReport.attacker;
                        component4.crit = Util.CheckRoll(damageReport.attackerBody.crit, damageReport.attackerMaster);
                        component4.remaining = (Blcount * 4);
                    }
                }
            }
        }
        #endregion
        #region CrimsonBison
        private void AddCrimsonBison()
        {
            ItemDef itemdef = new ItemDef
            {
                name = "CRIMSONBISON_NAME",
                nameToken = "CRIMSONBISON_NAME",
                pickupToken = "CRIMSONBISON_PICKUP",
                descriptionToken = "CRIMSONBISON_DESC",
                //loreToken = "CRIMSONBISON_LORE",
                tier = ItemTier.NoTier,
                pickupIconPath = "Textures/MiscIcons/texMysteryIcon",
                pickupModelPath = "Prefabs/PickupModels/PickupMystery",
                //Can remove determines if a shrine of order, or a printer can take this item, generally true, except for NoTier items.
                canRemove = false,
                //Hidden means that there will be no pickup notification, and it won't appear in the inventory at the top of the screen. This is useful for certain noTier helper items, such as the DrizzlePlayerHelper.
                hidden = false
            };

            var displayRules = new ItemDisplayRuleDict(null);

            CrimsonBison = ItemAPI.Add(new CustomItem(itemdef, displayRules));
            Tuple<ItemIndex, ItemIndex> requirements = new Tuple<ItemIndex, ItemIndex>(ItemIndex.SprintBonus, ItemIndex.Feather);
            comboRequirement.Add(CrimsonBison, requirements);

            R2API.LanguageAPI.Add("CRIMSONBISON_NAME", "Crimson Bison");
            R2API.LanguageAPI.Add("CRIMSONBISON_PICKUP", "Fly faster");
            R2API.LanguageAPI.Add("CRIMSONBISON_DESC", "Overall movespeed is increased by 60% <style=cStack>(+30% per stack)</style> while in the air.");
            //R2API.LanguageAPI.Add("CRIMSONBISON_LORE", "");

            On.RoR2.CharacterBody.RecalculateStats += (orig, self) =>
            {
                orig(self);
                if (self.HasBuff(bisonBuffIndex))
                {
                    int itemcount = self.inventory.GetItemCount(CrimsonBison);
                    float buffAmount = 1.30f + (0.30f * itemcount);
                    Reflection.SetPropertyValue<float>(self, "moveSpeed", self.moveSpeed * buffAmount);
                    Reflection.SetPropertyValue<float>(self, "acceleration", self.acceleration * 2f * buffAmount);
                }
            };
            
            On.RoR2.CharacterMotor.OnLeaveStableGround += (orig, self) =>
            { 
                orig(self);
                CharacterBody characterBody = Reflection.GetFieldValue<CharacterBody>(self, "body");
                if (characterBody && characterBody.inventory && characterBody.inventory.GetItemCount(CrimsonBison) > 0 && !characterBody.HasBuff(bisonBuffIndex))
                {
                    characterBody.AddBuff(bisonBuffIndex);
                }
            };

            On.RoR2.CharacterMotor.OnLanded += (orig, self) =>
            {
                orig(self);
                CharacterBody characterBody = Reflection.GetFieldValue<CharacterBody>(self, "body");
                if (characterBody && characterBody.inventory && characterBody.inventory.GetItemCount(CrimsonBison) > 0 && characterBody.HasBuff(bisonBuffIndex))
                {
                    characterBody.RemoveBuff(bisonBuffIndex);
                }
            };
        }
        private static BuffDef bisonBuffDef = new BuffDef
        {
            name = "Bison",
            iconPath = "Textures/BuffIcons/texBuffTempestSpeedIcon",
            buffColor = Color.red,
            canStack = false,
            isDebuff = false,
            eliteIndex = EliteIndex.None
        };
        private static CustomBuff bisonBuff = new CustomBuff(bisonBuffDef);
        public static BuffIndex bisonBuffIndex = BuffAPI.Add(bisonBuff);
        #endregion
        #region ScorchedEarth
        private void AddScorchedEarth()
        {
            //TODO: make this work
            ItemDef itemdef = new ItemDef
            {
                name = "SCORCHEDEARTH_NAME",
                nameToken = "SCORCHEDEARTH_NAME",
                pickupToken = "SCORCHEDEARTH_PICKUP",
                descriptionToken = "SCORCHEDEARTH_DESC",
                //loreToken = "SCORCHEDEARTH_LORE",
                tier = ItemTier.NoTier,
                pickupIconPath = "Textures/MiscIcons/texMysteryIcon",
                pickupModelPath = "Prefabs/PickupModels/PickupMystery",
                //Can remove determines if a shrine of order, or a printer can take this item, generally true, except for NoTier items.
                canRemove = false,
                //Hidden means that there will be no pickup notification, and it won't appear in the inventory at the top of the screen. This is useful for certain noTier helper items, such as the DrizzlePlayerHelper.
                hidden = false
            };

            var displayRules = new ItemDisplayRuleDict(null);

            ScorchedEarth = ItemAPI.Add(new CustomItem(itemdef, displayRules));
            Tuple<ItemIndex, ItemIndex> requirements = new Tuple<ItemIndex, ItemIndex>(ItemIndex.IgniteOnKill, ItemIndex.ExplodeOnDeath);
            comboRequirement.Add(ScorchedEarth, requirements);

            R2API.LanguageAPI.Add("SCORCHEDEARTH_NAME", "Scorched Earth");
            R2API.LanguageAPI.Add("SCORCHEDEARTH_PICKUP", "Burn the ground on kills");
            R2API.LanguageAPI.Add("SCORCHEDEARTH_DESC", "Set the ground on fire with in a 12m radius <style=cStack>(+4m per stack)</style> after every kill.");
            //R2API.LanguageAPI.Add("ScorchedEarth_LORE", "");

            RoR2.GlobalEventManager.onCharacterDeathGlobal += ScorchEarth;
        }

        static GameObject fireTrailPrefab = Resources.Load<GameObject>("Prefabs/FireTrail");
        private void ScorchEarth(DamageReport damageReport)
        {
            if (damageReport.attackerBody && damageReport.attackerBody.inventory & damageReport.victimBody)
            {
                int itemCount = damageReport.attackerBody.inventory.GetItemCount(ScorchedEarth);
                if (itemCount > 0)
                {
                    DamageTrail fireTrail = UnityEngine.Object.Instantiate<GameObject>(fireTrailPrefab, damageReport.victim.transform).GetComponent<DamageTrail>();
                    fireTrail.transform.position = damageReport.victimBody.footPosition;
                    fireTrail.owner = damageReport.attacker;
                    fireTrail.radius *= 8f + (4f * itemCount);
                }
            }
        }
        #endregion
        #region MustardGas
        private void AddMustardGas()
        {
            ItemDef itemdef = new ItemDef
            {
                name = "MUSTARDGAS_NAME",
                nameToken = "MUSTARDGAS_NAME",
                pickupToken = "MUSTARDGAS_PICKUP",
                descriptionToken = "MUSTARDGAS_DESC",
                //loreToken = "MUSTARDGAS_LORE",
                tier = ItemTier.NoTier,
                pickupIconPath = "Textures/MiscIcons/texMysteryIcon",
                pickupModelPath = "Prefabs/PickupModels/PickupMystery",
                //Can remove determines if a shrine of order, or a printer can take this item, generally true, except for NoTier items.
                canRemove = false,
                //Hidden means that there will be no pickup notification, and it won't appear in the inventory at the top of the screen. This is useful for certain noTier helper items, such as the DrizzlePlayerHelper.
                hidden = false
            };

            var displayRules = new ItemDisplayRuleDict(null);

            MustardGas = ItemAPI.Add(new CustomItem(itemdef, displayRules));
            Tuple<ItemIndex, ItemIndex> requirements = new Tuple<ItemIndex, ItemIndex>(ItemIndex.StunChanceOnHit, ItemIndex.SlowOnHit);
            comboRequirement.Add(MustardGas, requirements);

            R2API.LanguageAPI.Add("MUSTARDGAS_NAME", "Mustard Gas");
            R2API.LanguageAPI.Add("MUSTARDGAS_PICKUP", "Reduce attack speed on hit");
            R2API.LanguageAPI.Add("MUSTARDGAS_DESC", "Reduce attack speed of enemies on hit for -30% attack speed for 2s <style=cStack>(+2s per stack)</style>.");
            //R2API.LanguageAPI.Add("MUSTARDGAS_LORE", "");

            On.RoR2.CharacterBody.RecalculateStats += (orig, self) =>
            {
                orig(self);
                if (self.HasBuff(mustardBuffIndex))
                {
                    Reflection.SetPropertyValue<float>(self, "attackSpeed", self.attackSpeed * 0.7f);
                }
            };

            GlobalEventManager.onServerDamageDealt += smearMustard;
        }

        private void smearMustard(DamageReport damageReport)
        {
            if (damageReport.attackerBody && damageReport.attackerBody.inventory && damageReport.victimBody)
            {
                int itemCount = damageReport.attackerBody.inventory.GetItemCount(MustardGas);
                if (itemCount > 0 && damageReport.damageInfo.procCoefficient > 0)
                {
                    float duration = 2 * itemCount * damageReport.damageInfo.procCoefficient;
                    damageReport.victimBody.AddTimedBuff(mustardBuffIndex, duration);
                }
            }
        }

        private static BuffDef mustardDef = new BuffDef
        {
            name = "Mustard",
            iconPath = "Textures/BuffIcons/texBuffBleedingIcon",
            buffColor = Color.yellow,
            canStack = false,
            isDebuff = false,
            eliteIndex = EliteIndex.None
        };
        private static CustomBuff mustardBuff = new CustomBuff(mustardDef);
        public static BuffIndex mustardBuffIndex = BuffAPI.Add(mustardBuff);
        #endregion
        #region CyberneticInfusion
        private void AddCyberneticInfusion()
        {
            ItemDef itemdef = new ItemDef
            {
                name = "CYBERNETICINFUSION_NAME",
                nameToken = "CYBERNETICINFUSION_NAME",
                pickupToken = "CYBERNETICINFUSION_PICKUP",
                descriptionToken = "CYBERNETICINFUSION_DESC",
                //loreToken = "CYBERNETICINFUSION_LORE",
                tier = ItemTier.NoTier,
                pickupIconPath = "Textures/MiscIcons/texMysteryIcon",
                pickupModelPath = "Prefabs/PickupModels/PickupMystery",
                //Can remove determines if a shrine of order, or a printer can take this item, generally true, except for NoTier items.
                canRemove = false,
                //Hidden means that there will be no pickup notification, and it won't appear in the inventory at the top of the screen. This is useful for certain noTier helper items, such as the DrizzlePlayerHelper.
                hidden = false
            };

            var displayRules = new ItemDisplayRuleDict(null);

            CyberneticInfusion = ItemAPI.Add(new CustomItem(itemdef, displayRules));
            Tuple<ItemIndex, ItemIndex> requirements = new Tuple<ItemIndex, ItemIndex>(ItemIndex.Infusion, ItemIndex.PersonalShield);
            comboRequirement.Add(CyberneticInfusion, requirements);

            R2API.LanguageAPI.Add("CYBERNETICINFUSION_NAME", "Cybernetic Infusion");
            R2API.LanguageAPI.Add("CYBERNETICINFUSION_PICKUP", "Grant half of infusion bonus as shield");
            R2API.LanguageAPI.Add("CYBERNETICINFUSION_DESC", "Gain +50% <style=cStack>(+50% per stack)</style> of infusion bonus as shield");
            //R2API.LanguageAPI.Add("CYBERNETICINFUSION_LORE", "");

            On.RoR2.CharacterBody.RecalculateStats += (orig, self) =>
            {
                orig(self);
                if (self.inventory)
                {
                    int itemcount = self.inventory.GetItemCount(CyberneticInfusion);
                    if (itemcount > 0)
                    {
                        Reflection.SetPropertyValue<float>(self, "maxShield", self.maxShield + (self.inventory.infusionBonus * 0.5f * itemcount));
                    }
                }
            };
        }
        #endregion
        #region RosePlating
        private void AddRosePlating()
        {
            ItemDef itemdef = new ItemDef
            {
                name = "ROSEPLATING_NAME",
                nameToken = "ROSEPLATING_NAME",
                pickupToken = "ROSEPLATING_PICKUP",
                descriptionToken = "ROSEPLATING_DESC",
                //loreToken = "ROSEPLATING_LORE",
                tier = ItemTier.NoTier,
                pickupIconPath = "Textures/MiscIcons/texMysteryIcon",
                pickupModelPath = "Prefabs/PickupModels/PickupMystery",
                //Can remove determines if a shrine of order, or a printer can take this item, generally true, except for NoTier items.
                canRemove = false,
                //Hidden means that there will be no pickup notification, and it won't appear in the inventory at the top of the screen. This is useful for certain noTier helper items, such as the DrizzlePlayerHelper.
                hidden = false
            };

            var displayRules = new ItemDisplayRuleDict(null);

            RosePlating = ItemAPI.Add(new CustomItem(itemdef, displayRules));
            Tuple<ItemIndex, ItemIndex> requirements = new Tuple<ItemIndex, ItemIndex>(ItemIndex.ArmorPlate, ItemIndex.SprintArmor);
            comboRequirement.Add(RosePlating, requirements);

            R2API.LanguageAPI.Add("ROSEPLATING_NAME", "Rose Plating");
            R2API.LanguageAPI.Add("ROSEPLATING_PICKUP", "Increase armor");
            R2API.LanguageAPI.Add("ROSEPLATING_DESC", "Gain 30 armor <style=cStack>(+30 per stack)</style>.");
            //R2API.LanguageAPI.Add("ROSEPLATING_LORE", "");

            On.RoR2.CharacterBody.RecalculateStats += (orig, self) =>
            {
                orig(self);
                if (self.inventory)
                {
                    int itemcount = self.inventory.GetItemCount(RosePlating);
                    if (itemcount > 0)
                    {
                        Reflection.SetPropertyValue<float>(self, "armor", self.armor + (30f * itemcount));
                    }
                }
            };
        }
        #endregion
        #region TheManifesto
        private void AddTheManifesto()
        {
            ItemDef itemdef = new ItemDef
            {
                name = "THEMANIFESTO_NAME",
                nameToken = "THEMANIFESTO_NAME",
                pickupToken = "THEMANIFESTO_PICKUP",
                descriptionToken = "THEMANIFESTO_DESC",
                //loreToken = "THEMANIFESTO_LORE",
                tier = ItemTier.NoTier,
                pickupIconPath = "Textures/MiscIcons/texMysteryIcon",
                pickupModelPath = "Prefabs/PickupModels/PickupMystery",
                //Can remove determines if a shrine of order, or a printer can take this item, generally true, except for NoTier items.
                canRemove = false,
                //Hidden means that there will be no pickup notification, and it won't appear in the inventory at the top of the screen. This is useful for certain noTier helper items, such as the DrizzlePlayerHelper.
                hidden = false
            };

            var displayRules = new ItemDisplayRuleDict(null);

            TheManifesto = ItemAPI.Add(new CustomItem(itemdef, displayRules));
            Tuple<ItemIndex, ItemIndex> requirements = new Tuple<ItemIndex, ItemIndex>(ItemIndex.ExecuteLowHealthElite, ItemIndex.DeathMark);
            comboRequirement.Add(TheManifesto, requirements);

            R2API.LanguageAPI.Add("THEMANIFESTO_NAME", "The Manifesto");
            R2API.LanguageAPI.Add("THEMANIFESTO_PICKUP", "Elite affixes now count as 2 debuffs");
            R2API.LanguageAPI.Add("THEMANIFESTO_DESC", "Elite affixes each count as 2 <style=cStack>(+1 per stack)</style> debuffs. The Manifesto also works as a Death Mark");
            //R2API.LanguageAPI.Add("THEMANIFESTO_LORE", "");

            On.RoR2.GlobalEventManager.OnHitEnemy += (orig, self, damageInfo, victim) =>
            {
                orig(self, damageInfo, victim);
                if (damageInfo.attacker)
                {
                    CharacterBody attackerBody = damageInfo.attacker.GetComponent<CharacterBody>();
                    if (attackerBody && attackerBody.inventory)
                    {
                        int itemcount = attackerBody.inventory.GetItemCount(TheManifesto);
                        if (itemcount > 0)
                        {
                            CharacterBody victimBody = victim.GetComponent<CharacterBody>();
                            if (victimBody && victimBody.isElite)
                            {
                                int itemCount10 = itemcount + attackerBody.master.inventory.GetItemCount(ItemIndex.DeathMark);
                                int num5 = 0;
                                if (itemCount10 >= 1 && !victimBody.HasBuff(BuffIndex.DeathMark))
                                {
                                    foreach (BuffIndex buffType in BuffCatalog.debuffBuffIndices)
                                    {
                                        if (victimBody.HasBuff(buffType))
                                        {
                                            num5++;
                                        }
                                    }
                                    DotController dotController = DotController.FindDotController(victim.gameObject);
                                    if (dotController)
                                    {
                                        for (DotController.DotIndex dotIndex = DotController.DotIndex.Bleed; dotIndex < DotController.DotIndex.Count; dotIndex++)
                                        {
                                            if (dotController.HasDotActive(dotIndex))
                                            {
                                                num5++;
                                            }
                                        }
                                    }
                                    num5 += 1 + itemcount;
                                    if (num5 >= 4)
                                    {
                                        victimBody.AddTimedBuff(BuffIndex.DeathMark, 7f * (float)itemCount10);
                                    }
                                }
                            }
                    }
                    }
                }
            };
        }
        #endregion
        #region BirdsOfAFeather
        private void AddBirdsOfAFeather()
        {
            ItemDef itemdef = new ItemDef
            {
                name = "BIRDSOFAFEATHER_NAME",
                nameToken = "BIRDSOFAFEATHER_NAME",
                pickupToken = "BIRDSOFAFEATHER_PICKUP",
                descriptionToken = "BIRDSOFAFEATHER_DESC",
                //loreToken = "BIRDSOFAFEATHER_LORE",
                tier = ItemTier.NoTier,
                pickupIconPath = "Textures/MiscIcons/texMysteryIcon",
                pickupModelPath = "Prefabs/PickupModels/PickupMystery",
                //Can remove determines if a shrine of order, or a printer can take this item, generally true, except for NoTier items.
                canRemove = false,
                //Hidden means that there will be no pickup notification, and it won't appear in the inventory at the top of the screen. This is useful for certain noTier helper items, such as the DrizzlePlayerHelper.
                hidden = false
            };

            var displayRules = new ItemDisplayRuleDict(null);

            BirdsOfAFeather = ItemAPI.Add(new CustomItem(itemdef, displayRules));
            Tuple<ItemIndex, ItemIndex> requirements = new Tuple<ItemIndex, ItemIndex>(ItemIndex.JumpBoost, ItemIndex.Feather);
            comboRequirement.Add(BirdsOfAFeather, requirements);

            R2API.LanguageAPI.Add("BIRDSOFAFEATHER_NAME", "Birds of a Feather");
            R2API.LanguageAPI.Add("BIRDSOFAFEATHER_PICKUP", "Gain +1 base jump count.");
            R2API.LanguageAPI.Add("BIRDSOFAFEATHER_DESC", "Gain <style=cIsUtility>+2</style> <style=cStack>(+2 per stack)</style> base <style=cIsUtility>jump count</style>.");
            //R2API.LanguageAPI.Add("BIRDSOFAFEATHER_LORE", "");

            On.RoR2.CharacterBody.RecalculateStats += (orig, self) =>
            {
                orig(self);
                if (self.inventory)
                {
                    int BFOAFcount = self.inventory.GetItemCount(BirdsOfAFeather);
                    if(BFOAFcount > 0)
                    {
                        self.baseJumpCount = (2 * BFOAFcount) + 1;
                        if (self.baseNameToken == "MERC_BODY_NAME") { self.baseJumpCount++; }
                    }                    
                }
            };
        }
        #endregion
        #region TheSpark
        private void AddTheSpark()
        {
            ItemDef itemdef = new ItemDef
            {
                name = "THESPARK_NAME",
                nameToken = "THESPARK_NAME",
                pickupToken = "THESPARK_PICKUP",
                descriptionToken = "THESPARK_DESC",
                //loreToken = "THESPARK_LORE",
                tier = ItemTier.NoTier,
                pickupIconPath = "Textures/MiscIcons/texMysteryIcon",
                pickupModelPath = "Prefabs/PickupModels/PickupMystery",
                //Can remove determines if a shrine of order, or a printer can take this item, generally true, except for NoTier items.
                canRemove = false,
                //Hidden means that there will be no pickup notification, and it won't appear in the inventory at the top of the screen. This is useful for certain noTier helper items, such as the DrizzlePlayerHelper.
                hidden = false
            };

            var displayRules = new ItemDisplayRuleDict(null);

            TheSpark = ItemAPI.Add(new CustomItem(itemdef, displayRules));
            Tuple<ItemIndex, ItemIndex> requirements = new Tuple<ItemIndex, ItemIndex>(ItemIndex.FireRing, ItemIndex.IceRing);
            comboRequirement.Add(TheSpark, requirements);

            R2API.LanguageAPI.Add("THESPARK_NAME", "The Spark");
            R2API.LanguageAPI.Add("THESPARK_PICKUP", "The air is electric when we're together");
            R2API.LanguageAPI.Add("THESPARK_DESC", "Activating bands will now fire chain lightning for 20% TOTAL damage on up to 6 <style=cStack>(+6 per stack)</style> targets within 50m <style=cStack>(+20m per stack)</style>.");
            //R2API.LanguageAPI.Add("THESPARK_LORE", "");

            On.RoR2.GlobalEventManager.OnHitEnemy += (orig, self, damageInfo, victim) =>
            {
                if (damageInfo.attacker)
                {
                    CharacterBody attackerBody = damageInfo.attacker.GetComponent<CharacterBody>();
                    if (attackerBody && attackerBody.inventory)
                    {
                        Inventory inventory = attackerBody.inventory;
                        int itemcount = inventory.GetItemCount(TheSpark);
                        if (itemcount > 0 && (damageInfo.damage / attackerBody.damage) > 4f && attackerBody.HasBuff(BuffIndex.ElementalRingsReady) && damageInfo.procCoefficient > 0)
                        {
                            int firecount = inventory.GetItemCount(ItemIndex.FireRing);
                            int icecount = inventory.GetItemCount(ItemIndex.IceRing);

                            float fireCoefficient = 2.5f * (float)firecount;
                            float iceCoefficient = 3f * (float)icecount;

                            TeamComponent component2 = attackerBody.GetComponent<TeamComponent>();
                            TeamIndex teamIndex = component2 ? component2.teamIndex : TeamIndex.Neutral;

                            float damageCoefficient2 = 0.2f + fireCoefficient + iceCoefficient;
                            float damageValue2 = Util.OnHitProcDamage(damageInfo.damage, attackerBody.damage, damageCoefficient2);
                            RoR2.Orbs.LightningOrb lightningOrb2 = new RoR2.Orbs.LightningOrb();
                            lightningOrb2.origin = damageInfo.position;
                            lightningOrb2.damageValue = damageValue2;
                            lightningOrb2.isCrit = damageInfo.crit;
                            lightningOrb2.bouncesRemaining = 6 * itemcount;
                            lightningOrb2.teamIndex = teamIndex;
                            lightningOrb2.attacker = damageInfo.attacker;
                            lightningOrb2.bouncedObjects = new List<HealthComponent>
                            {
                                victim.GetComponent<HealthComponent>()
                            };
                            lightningOrb2.procChainMask = damageInfo.procChainMask;
                            lightningOrb2.procChainMask.AddProc(ProcType.ChainLightning);
                            lightningOrb2.procCoefficient = 0.2f;
                            lightningOrb2.lightningType = RoR2.Orbs.LightningOrb.LightningType.Ukulele;
                            lightningOrb2.damageColorIndex = DamageColorIndex.Item;
                            lightningOrb2.range += (float)(30 + (20 * itemcount));
                            HurtBox hurtBox2 = lightningOrb2.PickNextTarget(damageInfo.position);
                            if (hurtBox2)
                            {
                                lightningOrb2.target = hurtBox2;
                                RoR2.Orbs.OrbManager.instance.AddOrb(lightningOrb2);
                            }
                        }
                    }
                }
                orig(self, damageInfo, victim);
            };
        }
        #endregion
        #region InvisibilityCloak
        private void AddInvisibilityCloak()
        {
            ItemDef itemdef = new ItemDef
            {
                name = "INVISIBILITYCLOAK_NAME",
                nameToken = "INVISIBILITYCLOAK_NAME",
                pickupToken = "INVISIBILITYCLOAK_PICKUP",
                descriptionToken = "INVISIBILITYCLOAK_DESC",
                //loreToken = "INVISIBILITYCLOAK_LORE",
                tier = ItemTier.NoTier,
                pickupIconPath = "Textures/MiscIcons/texMysteryIcon",
                pickupModelPath = "Prefabs/PickupModels/PickupMystery",
                //Can remove determines if a shrine of order, or a printer can take this item, generally true, except for NoTier items.
                canRemove = false,
                //Hidden means that there will be no pickup notification, and it won't appear in the inventory at the top of the screen. This is useful for certain noTier helper items, such as the DrizzlePlayerHelper.
                hidden = false
            };

            var displayRules = new ItemDisplayRuleDict(null);

            InvisiblityCloak = ItemAPI.Add(new CustomItem(itemdef, displayRules));
            Tuple<ItemIndex, ItemIndex> requirements = new Tuple<ItemIndex, ItemIndex>(ItemIndex.SprintOutOfCombat, ItemIndex.Phasing);
            comboRequirement.Add(InvisiblityCloak, requirements);

            R2API.LanguageAPI.Add("INVISIBILITYCLOAK_NAME", "Invisibility Cloak");
            R2API.LanguageAPI.Add("INVISIBILITYCLOAK_PICKUP", "Cloak while out of combat");
            R2API.LanguageAPI.Add("INVISIBILITYCLOAK_DESC", "Leaving combat turns you invisible."); //TODO: Figure out stacking
            //R2API.LanguageAPI.Add("INVISIBILITYCLOAK_LORE", "");

            On.RoR2.CharacterBody.RecalculateStats += (orig, self) =>
            {
                orig(self);
                if (self.inventory)
                {
                    int itemcount = self.inventory.GetItemCount(InvisiblityCloak);
                    if (itemcount > 0 && self.outOfCombat && !self.hasCloakBuff)
                    {
                        self.AddBuff(BuffIndex.Cloak);  //TODO: Add fx
                    }
                    if (self.hasCloakBuff && !self.outOfCombat && !self.HasBuff(BuffIndex.CloakSpeed))
                    {
                        self.RemoveBuff(BuffIndex.Cloak);
                    }
                }
            };

            On.RoR2.CharacterBody.AddTimedBuff += (orig, self, index, duration) =>
            {
                if(index == BuffIndex.Cloak && self.hasCloakBuff)
                {
                    return;
                }
                orig(self, index, duration);
            };
        }
        #endregion
        #region TheCollectiveUnconscious
        private void AddTheCollectiveUnconscious()
        {
            ItemDef itemdef = new ItemDef
            {
                name = "THECOLLECTIVEUNCONSCIOUS_NAME",
                nameToken = "THECOLLECTIVEUNCONSCIOUS_NAME",
                pickupToken = "THECOLLECTIVEUNCONSCIOUS_PICKUP",
                descriptionToken = "THECOLLECTIVEUNCONSCIOUS_DESC",
                //loreToken = "THECOLLECTIVEUNCONSCIOUS_LORE",
                tier = ItemTier.NoTier,
                pickupIconPath = "Textures/MiscIcons/texMysteryIcon",
                pickupModelPath = "Prefabs/PickupModels/PickupMystery",
                //Can remove determines if a shrine of order, or a printer can take this item, generally true, except for NoTier items.
                canRemove = false,
                //Hidden means that there will be no pickup notification, and it won't appear in the inventory at the top of the screen. This is useful for certain noTier helper items, such as the DrizzlePlayerHelper.
                hidden = false
            };

            var displayRules = new ItemDisplayRuleDict(null);

            TheCollectiveUnconscious = ItemAPI.Add(new CustomItem(itemdef, displayRules));
            Tuple<ItemIndex, ItemIndex> requirements = new Tuple<ItemIndex, ItemIndex>(ItemIndex.WardOnLevel, ItemIndex.KillEliteFrenzy);
            comboRequirement.Add(TheCollectiveUnconscious, requirements);

            R2API.LanguageAPI.Add("THECOLLECTIVEUNCONSCIOUS_NAME", "The Collective Unconscious");
            R2API.LanguageAPI.Add("THECOLLECTIVEUNCONSCIOUS_PICKUP", "Produce a thought that sends everyone into a frenzy.");
            R2API.LanguageAPI.Add("THECOLLECTIVEUNCONSCIOUS_DESC", "On champion kills, drop an artifact that sends all allies within 16m into a frenzy where skills have 0.5s cooldowns. Lasts for 8 <style=cStack>(+8 per stack)</style> seconds.");
            //R2API.LanguageAPI.Add("THECOLLECTIVEUNCONSCIOUS_LORE", "");

            GlobalEventManager.onCharacterDeathGlobal += dropCooldownWard;
        }

        private void dropCooldownWard(DamageReport damageReport)
        {
            if (damageReport.attackerBody && damageReport.attackerBody.inventory)
            {
                int itemCount = damageReport.attackerBody.inventory.GetItemCount(TheCollectiveUnconscious);
                if (itemCount > 0 && damageReport.victimIsElite)
                {
                    GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/NetworkedObjects/WarbannerWard"), damageReport.attackerBody.transform.position, Quaternion.identity);
                    gameObject.GetComponent<TeamFilter>().teamIndex = damageReport.attackerTeamIndex;
                    gameObject.GetComponent<BuffWard>().Networkradius = 16f;
                    UnityEngine.Networking.NetworkServer.Spawn(gameObject);
                }
            }
        }
        #endregion
        #region CrownOfFrost
        private void AddCrownOfFrost()
        {
            ItemDef itemdef = new ItemDef
            {
                name = "CROWNOFFROST_NAME",
                nameToken = "CROWNOFFROST_NAME",
                pickupToken = "CROWNOFFROST_PICKUP",
                descriptionToken = "CROWNOFFROST_DESC",
                //loreToken = "CROWNOFFROST_LORE",
                tier = ItemTier.NoTier,
                pickupIconPath = "Textures/MiscIcons/texMysteryIcon",
                pickupModelPath = "Prefabs/PickupModels/PickupMystery",
                //Can remove determines if a shrine of order, or a printer can take this item, generally true, except for NoTier items.
                canRemove = false,
                //Hidden means that there will be no pickup notification, and it won't appear in the inventory at the top of the screen. This is useful for certain noTier helper items, such as the DrizzlePlayerHelper.
                hidden = false
            };

            var displayRules = new ItemDisplayRuleDict(null);

            CrownOfFrost = ItemAPI.Add(new CustomItem(itemdef, displayRules));
            Tuple<ItemIndex, ItemIndex> requirements = new Tuple<ItemIndex, ItemIndex>(ItemIndex.StunChanceOnHit, ItemIndex.HeadHunter);
            comboRequirement.Add(CrownOfFrost, requirements);

            R2API.LanguageAPI.Add("CROWNOFFROST_NAME", "Crown of Frost");
            R2API.LanguageAPI.Add("CROWNOFFROST_PICKUP", "Gain the powers of a Glacial Elite");
            R2API.LanguageAPI.Add("CROWNOFFROST_DESC", "Gain Glacial Elite affix."); //TODO: figure out stacking
            //R2API.LanguageAPI.Add("CROWNOFFROST_LORE", "");

            On.RoR2.CharacterBody.RecalculateStats += (orig, self) =>
            {
                orig(self);
                if (self.inventory)
                {
                    int itemcount = self.inventory.GetItemCount(CrownOfFrost);
                    if (itemcount > 0 && !self.HasBuff(BuffIndex.AffixWhite))
                    {
                        self.AddBuff(BuffIndex.AffixWhite);
                    }
                }
            };
        }
        #endregion
        #region LuckyKey
        private void AddLuckyKey()
        {
            ItemDef itemdef = new ItemDef
            {
                name = "LUCKYKEY_NAME",
                nameToken = "LUCKYKEY_NAME",
                pickupToken = "LUCKYKEY_PICKUP",
                descriptionToken = "LUCKYKEY_DESC",
                //loreToken = "LUCKYKEY_LORE",
                tier = ItemTier.NoTier,
                pickupIconPath = "Textures/MiscIcons/texMysteryIcon",
                pickupModelPath = "Prefabs/PickupModels/PickupMystery",
                //Can remove determines if a shrine of order, or a printer can take this item, generally true, except for NoTier items.
                canRemove = false,
                //Hidden means that there will be no pickup notification, and it won't appear in the inventory at the top of the screen. This is useful for certain noTier helper items, such as the DrizzlePlayerHelper.
                hidden = false
            };

            var displayRules = new ItemDisplayRuleDict(null);

            LuckyKey = ItemAPI.Add(new CustomItem(itemdef, displayRules));
            Tuple<ItemIndex, ItemIndex> requirements = new Tuple<ItemIndex, ItemIndex>(ItemIndex.TreasureCache, ItemIndex.Clover);
            comboRequirement.Add(LuckyKey, requirements);

            R2API.LanguageAPI.Add("LUCKYKEY_NAME", "Lucky Key");
            R2API.LanguageAPI.Add("LUCKYKEY_PICKUP", "Fits in any hole");
            R2API.LanguageAPI.Add("LUCKYKEY_DESC", "3% chance <style=cStack>(+3% per stack)</style> to have an item drop on kill");
            //R2API.LanguageAPI.Add("LUCKYKEY_LORE", "");

            GlobalEventManager.onCharacterDeathGlobal += luckyKeyProc;
        }

        static PickupDropTable dropTable = Resources.Load<PickupDropTable>("DropTables/dtSacrificeArtifact");

        private void luckyKeyProc(DamageReport damageReport)
        {
            if (damageReport.attackerBody && damageReport.attackerBody.inventory)
            {
                int itemcount = damageReport.attackerBody.inventory.GetItemCount(LuckyKey);
                if (itemcount > 0)
                {
                    bool itfits = Util.CheckRoll(5 * itemcount, damageReport.attackerMaster);
                    if (itfits)
                    {
                        Xoroshiro128Plus treasureRng = new Xoroshiro128Plus((ulong)UnityEngine.Random.Range(0, 100));
                        PickupIndex pickupIndex = dropTable.GenerateDrop(treasureRng);
                        if (pickupIndex != PickupIndex.none)
                        {
                            PickupDropletController.CreatePickupDroplet(pickupIndex, damageReport.victimBody.corePosition, Vector3.up * 20f);
                        }
                    }
                }
            }
        }
        #endregion
        #region TheHardStuff
        private void AddTheHardStuff()
        {
            ItemDef itemdef = new ItemDef
            {
                name = "THEHARDSTUFF_NAME",
                nameToken = "THEHARDSTUFF_NAME",
                pickupToken = "THEHARDSTUFF_PICKUP",
                descriptionToken = "THEHARDSTUFF_DESC",
                //loreToken = "THEHARDSTUFF_LORE",
                tier = ItemTier.NoTier,
                pickupIconPath = "Textures/MiscIcons/texMysteryIcon",
                pickupModelPath = "Prefabs/PickupModels/PickupMystery",
                //Can remove determines if a shrine of order, or a printer can take this item, generally true, except for NoTier items.
                canRemove = false,
                //Hidden means that there will be no pickup notification, and it won't appear in the inventory at the top of the screen. This is useful for certain noTier helper items, such as the DrizzlePlayerHelper.
                hidden = false
            };

            var displayRules = new ItemDisplayRuleDict(null);

            TheHardStuff = ItemAPI.Add(new CustomItem(itemdef, displayRules));
            Tuple<ItemIndex, ItemIndex> requirements = new Tuple<ItemIndex, ItemIndex>(ItemIndex.Syringe, ItemIndex.AlienHead);
            comboRequirement.Add(TheHardStuff, requirements);

            R2API.LanguageAPI.Add("THEHARDSTUFF_NAME", "The Hard Stuff");
            R2API.LanguageAPI.Add("THEHARDSTUFF_PICKUP", "Reduce cooldowns with attack speed");
            R2API.LanguageAPI.Add("THEHARDSTUFF_DESC", "Reduces cooldowns with attack speed. Extra stacks increase reduction linearly. Has no effect if your attack speed is less than base");
            //R2API.LanguageAPI.Add("THEHARDSTUFF_LORE", "");

            On.RoR2.CharacterBody.RecalculateStats += (orig, self) =>
            {
                orig(self);
                if (self.inventory)
                {
                    int itemcount = self.inventory.GetItemCount(TheHardStuff);
                    float atkSpeedBonus = (self.attackSpeed / self.baseAttackSpeed) - 1;
                    if (itemcount > 0 && atkSpeedBonus > 0)
                    {
                        float reduction = (2 / ((itemcount * atkSpeedBonus) + 2));

                        if (self.skillLocator.primary)
                        {
                            self.skillLocator.primary.cooldownScale = reduction;
                        }
                        if (self.skillLocator.secondary)
                        {
                            self.skillLocator.secondary.cooldownScale = reduction;
                        }
                        if (self.skillLocator.utility)
                        {
                            self.skillLocator.utility.cooldownScale = reduction;
                        }
                        if (self.skillLocator.special)
                        {
                            self.skillLocator.special.cooldownScale = reduction;
                        }
                    }
                }
            };
        }
        #endregion
        #region BioFuelCell
        private bool wasProc = false;
        private void AddBioFuelCell()
        {
            ItemDef itemdef = new ItemDef
            {
                name = "BIOFUELCELL_NAME",
                nameToken = "BIOFUELCELL_NAME",
                pickupToken = "BIOFUELCELL_PICKUP",
                descriptionToken = "BIOFUELCELL_DESC",
                //loreToken = "BIOFUELCELL_LORE",
                tier = ItemTier.NoTier,
                pickupIconPath = "Textures/MiscIcons/texMysteryIcon",
                pickupModelPath = "Prefabs/PickupModels/PickupMystery",
                //Can remove determines if a shrine of order, or a printer can take this item, generally true, except for NoTier items.
                canRemove = false,
                //Hidden means that there will be no pickup notification, and it won't appear in the inventory at the top of the screen. This is useful for certain noTier helper items, such as the DrizzlePlayerHelper.
                hidden = false
            };

            var displayRules = new ItemDisplayRuleDict(null);

            BioFuelCell = ItemAPI.Add(new CustomItem(itemdef, displayRules));
            Tuple<ItemIndex, ItemIndex> requirements = new Tuple<ItemIndex, ItemIndex>(ItemIndex.Clover, ItemIndex.EquipmentMagazine);
            comboRequirement.Add(BioFuelCell, requirements);

            R2API.LanguageAPI.Add("BIOFUELCELL_NAME", "BioFuel Cell");
            R2API.LanguageAPI.Add("BIOFUELCELL_PICKUP", "Chance to activate equpment on hit");
            R2API.LanguageAPI.Add("BIOFUELCELL_DESC", "5% chance <style=cStack>(+5% per stack)</style> to activate your equipment at no charge cost.");
            //R2API.LanguageAPI.Add("BIOFUELCELL_LORE", "");

            GlobalEventManager.onServerDamageDealt += BioFuelProc;

            On.RoR2.Inventory.DeductEquipmentCharges += (orig, self, slot, deduction) =>
            {
                if (wasProc)
                {
                    wasProc = false;
                    return;
                }
                orig(self, slot, deduction);
            };
        }

        private void BioFuelProc(DamageReport damageReport)
        {
            if (damageReport.attackerBody && damageReport.attackerBody.inventory)
            {
                Inventory inventory = damageReport.attackerBody.inventory;
                int itemcount = inventory.GetItemCount(BioFuelCell);
                if (itemcount > 0 && damageReport.attackerBody.equipmentSlot.equipmentIndex != EquipmentIndex.None && damageReport.damageInfo.procCoefficient > 0)
                {
                    float chanceToProc = 5 * itemcount * damageReport.damageInfo.procCoefficient;
                    bool activate = Util.CheckRoll(chanceToProc, damageReport.attackerMaster);
                    if (activate)
                    {
                        wasProc = true;
                        damageReport.attackerBody.equipmentSlot.InvokeMethod("Execute");
                    }
                }
            }
        }
        #endregion
        #region CrownOfEmbers
        private void AddCrownOfEmbers()
        {
            ItemDef itemdef = new ItemDef
            {
                name = "CROWNOFEMBERS_NAME",
                nameToken = "CROWNOFEMBERS_NAME",
                pickupToken = "CROWNOFEMBERS_PICKUP",
                descriptionToken = "CROWNOFEMBERS_DESC",
                //loreToken = "CROWNOFEMBERS_LORE",
                tier = ItemTier.NoTier,
                pickupIconPath = "Textures/MiscIcons/texMysteryIcon",
                pickupModelPath = "Prefabs/PickupModels/PickupMystery",
                //Can remove determines if a shrine of order, or a printer can take this item, generally true, except for NoTier items.
                canRemove = false,
                //Hidden means that there will be no pickup notification, and it won't appear in the inventory at the top of the screen. This is useful for certain noTier helper items, such as the DrizzlePlayerHelper.
                hidden = false
            };

            var displayRules = new ItemDisplayRuleDict(null);

            CrownOfEmbers = ItemAPI.Add(new CustomItem(itemdef, displayRules));
            Tuple<ItemIndex, ItemIndex> requirements = new Tuple<ItemIndex, ItemIndex>(ItemIndex.FireRing, ItemIndex.HeadHunter);
            comboRequirement.Add(CrownOfEmbers, requirements);

            R2API.LanguageAPI.Add("CROWNOFEMBERS_NAME", "Crown of Embers");
            R2API.LanguageAPI.Add("CROWNOFEMBERS_PICKUP", "Gain the powers of a Blazing Elite");
            R2API.LanguageAPI.Add("CROWNOFEMBERS_DESC", "Gain Blazing Elite affix."); //TODO: figure out stacking
            //R2API.LanguageAPI.Add("CROWNOFEMBERS_LORE", "");

            On.RoR2.CharacterBody.RecalculateStats += (orig, self) =>
            {
                orig(self);
                if (self.inventory)
                {
                    int itemcount = self.inventory.GetItemCount(CrownOfEmbers);
                    if (itemcount > 0 && !self.HasBuff(BuffIndex.AffixRed))
                    {
                        self.AddBuff(BuffIndex.AffixRed);
                    }
                }
            };
        }
        #endregion
        #region AnniversaryGift
        private GameObject teleporter;
        private void AddAnniversaryGift()
        {
            ItemDef itemdef = new ItemDef
            {
                name = "ANNIVERSARYGIFT_NAME",
                nameToken = "ANNIVERSARYGIFT_NAME",
                pickupToken = "ANNIVERSARYGIFT_PICKUP",
                descriptionToken = "ANNIVERSARYGIFT_DESC",
                //loreToken = "ANNIVERSARYGIFT_LORE",
                tier = ItemTier.NoTier,
                pickupIconPath = "Textures/MiscIcons/texMysteryIcon",
                pickupModelPath = "Prefabs/PickupModels/PickupMystery",
                //Can remove determines if a shrine of order, or a printer can take this item, generally true, except for NoTier items.
                canRemove = false,
                //Hidden means that there will be no pickup notification, and it won't appear in the inventory at the top of the screen. This is useful for certain noTier helper items, such as the DrizzlePlayerHelper.
                hidden = false
            };

            var displayRules = new ItemDisplayRuleDict(null);

            AnniversaryGift = ItemAPI.Add(new CustomItem(itemdef, displayRules));
            Tuple<ItemIndex, ItemIndex> requirements = new Tuple<ItemIndex, ItemIndex>(ItemIndex.ExtraLife, ItemIndex.TPHealingNova);
            comboRequirement.Add(AnniversaryGift, requirements);

            R2API.LanguageAPI.Add("ANNIVERSARYGIFT_NAME", "Anniversary Gift");
            R2API.LanguageAPI.Add("ANNIVERSARYGIFT_PICKUP", "Revive once per teleporter event if you die in the teleporter zone");
            R2API.LanguageAPI.Add("ANNIVERSARYGIFT_DESC", "Revive once per teleporter event <style=cStack>(+1 per stack)</style> if you die in the teleporter zone.");
            //R2API.LanguageAPI.Add("ANNIVERSARYGIFT_LORE", "");

            AddFlowerPetal();

            GlobalEventManager.onCharacterDeathGlobal += revive;

            On.RoR2.Run.OnServerTeleporterPlaced += (orig, self, sceneDirector, teleporter) =>
            {
                this.teleporter = teleporter;
                orig(self, sceneDirector, teleporter);
            };
        }

        public ItemIndex FlowerPetal;

        private void AddFlowerPetal()
        {
            ItemDef itemdef = new ItemDef
            {
                name = "FLOWERPETAL_NAME",
                nameToken = "FLOWERPETAL_NAME",
                pickupToken = "FLOWERPETAL_PICKUP",
                descriptionToken = "FLOWERPETAL_DESC",
                //loreToken = "FLOWERPETAL_LORE",
                tier = ItemTier.NoTier,
                pickupIconPath = "Textures/MiscIcons/texMysteryIcon",
                pickupModelPath = "Prefabs/PickupModels/PickupMystery",
                //Can remove determines if a shrine of order, or a printer can take this item, generally true, except for NoTier items.
                canRemove = false,
                //Hidden means that there will be no pickup notification, and it won't appear in the inventory at the top of the screen. This is useful for certain noTier helper items, such as the DrizzlePlayerHelper.
                hidden = false
            };

            var displayRules = new ItemDisplayRuleDict(null);

            FlowerPetal = ItemAPI.Add(new CustomItem(itemdef, displayRules));

            R2API.LanguageAPI.Add("FLOWERPETAL_NAME", "Flower Petal");
            R2API.LanguageAPI.Add("FLOWERPETAL_PICKUP", "A wilted remain of your gift");
            R2API.LanguageAPI.Add("FLOWERPETAL_DESC", "A wilted remain of your gift.");
            //R2API.LanguageAPI.Add("ANNIVERSARYGIFT_LORE", "");

            On.RoR2.SceneExitController.Begin += (orig, self) =>
            {
                for (int i = 0; i < PlayerCharacterMasterController.instances.Count; i++)
                {
                    Inventory inventory = PlayerCharacterMasterController.instances[i].master.inventory;
                    int itemcount = inventory.GetItemCount(FlowerPetal);
                    for (int j = 0; j < itemcount; j++)
                    {
                        inventory.RemoveItem(FlowerPetal);
                    }
                }
                orig(self);
            };
        }

        private void revive(DamageReport damageReport)
        {
            if (damageReport.victimBody && damageReport.victimBody.inventory)
            {
                int itemcount = damageReport.victimBody.inventory.GetItemCount(AnniversaryGift);
                if (itemcount > 0)
                {
                    int petalcount = damageReport.victimBody.inventory.GetItemCount(FlowerPetal);
                    TeleporterInteraction teleporterInteraction = teleporter.GetComponent<TeleporterInteraction>();
                    bool isChargingTeleporter = teleporterInteraction.isCharging && teleporterInteraction.holdoutZoneController.IsBodyInChargingRadius(damageReport.victimBody);
                    if (itemcount > petalcount && isChargingTeleporter)
                    {
                        Reflection.SetFieldValue<Vector3>(damageReport.victimBody.master, "deathFootPosition", damageReport.victimBody.footPosition);
                        damageReport.victimBody.master.RespawnExtraLife();
                        damageReport.victimBody.inventory.RemoveItem(ItemIndex.ExtraLifeConsumed);
                        damageReport.victimBody.master.inventory.GiveItem(FlowerPetal);
                    }
                }
            }
        }
        #endregion
        #region CrownOfThunder
        private void AddCrownOfThunder()
        {
            ItemDef itemdef = new ItemDef
            {
                name = "CROWNOFTHUNDER_NAME",
                nameToken = "CROWNOFTHUNDER_NAME",
                pickupToken = "CROWNOFTHUNDER_PICKUP",
                descriptionToken = "CROWNOFTHUNDER_DESC",
                //loreToken = "CROWNOFTHUNDER_LORE",
                tier = ItemTier.NoTier,
                pickupIconPath = "Textures/MiscIcons/texMysteryIcon",
                pickupModelPath = "Prefabs/PickupModels/PickupMystery",
                //Can remove determines if a shrine of order, or a printer can take this item, generally true, except for NoTier items.
                canRemove = false,
                //Hidden means that there will be no pickup notification, and it won't appear in the inventory at the top of the screen. This is useful for certain noTier helper items, such as the DrizzlePlayerHelper.
                hidden = false
            };

            var displayRules = new ItemDisplayRuleDict(null);

            CrownOfThunder = ItemAPI.Add(new CustomItem(itemdef, displayRules));
            Tuple<ItemIndex, ItemIndex> requirements = new Tuple<ItemIndex, ItemIndex>(ItemIndex.ChainLightning, ItemIndex.HeadHunter);
            comboRequirement.Add(CrownOfThunder, requirements);

            R2API.LanguageAPI.Add("CROWNOFTHUNDER_NAME", "Crown of Thunder");
            R2API.LanguageAPI.Add("CROWNOFTHUNDER_PICKUP", "Gain the powers of an Overloading Elite");
            R2API.LanguageAPI.Add("CROWNOFTHUNDER_DESC", "Gain Overloading Elite affix."); //TODO: figure out stacking
            //R2API.LanguageAPI.Add("CROWNOFEMBERS_LORE", "");

            On.RoR2.CharacterBody.RecalculateStats += (orig, self) =>
            {
                orig(self);
                if (self.inventory)
                {
                    int itemcount = self.inventory.GetItemCount(CrownOfThunder);
                    if (itemcount > 0 && !self.HasBuff(BuffIndex.AffixBlue))
                    {
                        self.AddBuff(BuffIndex.AffixBlue);
                    }
                }
            };
        }
        #endregion
        #region LoyalSoldiers
        private void AddLoyalSoldiers()
        {
            ItemDef itemdef = new ItemDef
            {
                name = "LOYALSOLDIERS_NAME",
                nameToken = "LOYALSOLDIERS_NAME",
                pickupToken = "LOYALSOLDIERS_PICKUP",
                descriptionToken = "LOYALSOLDIERS_DESC",
                //loreToken = "LOYALSOLDIERS_LORE",
                tier = ItemTier.NoTier,
                pickupIconPath = "Textures/MiscIcons/texMysteryIcon",
                pickupModelPath = "Prefabs/PickupModels/PickupMystery",
                //Can remove determines if a shrine of order, or a printer can take this item, generally true, except for NoTier items.
                canRemove = false,
                //Hidden means that there will be no pickup notification, and it won't appear in the inventory at the top of the screen. This is useful for certain noTier helper items, such as the DrizzlePlayerHelper.
                hidden = false
            };

            var displayRules = new ItemDisplayRuleDict(null);

            LoyalSoldiers = ItemAPI.Add(new CustomItem(itemdef, displayRules));
            Tuple<ItemIndex, ItemIndex> requirements = new Tuple<ItemIndex, ItemIndex>(ItemIndex.Squid, ItemIndex.NovaOnLowHealth);
            comboRequirement.Add(LoyalSoldiers, requirements);

            R2API.LanguageAPI.Add("LOYALSOLDIERS_NAME", "Loyal Soldiers");
            R2API.LanguageAPI.Add("LOYALSOLDIERS_PICKUP", "Squids have genesis loop.");
            R2API.LanguageAPI.Add("LOYALSOLDIERS_DESC", "The squids have a fucking genesis loop <style=cStack>(+fuck per stack)</style>.");
            //R2API.LanguageAPI.Add("LOYALSOLDIERS_LORE", "");

            On.RoR2.DirectorCore.TrySpawnObject += (orig, self, directorSpawnRequest) =>
            {
                GameObject gameObject = orig(self, directorSpawnRequest);

                SpawnCard squidCard = Resources.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscSquidTurret");
                if (directorSpawnRequest.spawnCard == squidCard && directorSpawnRequest.summonerBodyObject)
                {
                    CharacterBody characterBody = directorSpawnRequest.summonerBodyObject.GetComponent<CharacterBody>();
                    if (characterBody && characterBody.inventory)
                    {
                        int itemcount = characterBody.inventory.GetItemCount(LoyalSoldiers);
                        for (int i = 0; i < itemcount; i++)
                        {
                            gameObject.GetComponent<CharacterMaster>().inventory.GiveItem(ItemIndex.NovaOnLowHealth);
                        }
                    }
                }

                return gameObject;
            };
        }
        #endregion
        #region Overloading Perforator
        private void AddOverloadingPerforator()
        {
            ItemDef itemdef = new ItemDef
            {
                name = "OVERLOADINGPERFORATOR_NAME",
                nameToken = "OVERLOADINGPERFORATOR_NAME",
                pickupToken = "OVERLOADINGPERFORATOR_PICKUP",
                descriptionToken = "OVERLOADINGPERFORATOR_DESC",
                //loreToken = "OVERLOADINGPERFORATOR_LORE",
                tier = ItemTier.NoTier,
                pickupIconPath = "Textures/MiscIcons/texMysteryIcon",
                pickupModelPath = "Prefabs/PickupModels/PickupMystery",
                //Can remove determines if a shrine of order, or a printer can take this item, generally true, except for NoTier items.
                canRemove = false,
                //Hidden means that there will be no pickup notification, and it won't appear in the inventory at the top of the screen. This is useful for certain noTier helper items, such as the DrizzlePlayerHelper.
                hidden = false
            };

            var displayRules = new ItemDisplayRuleDict(null);

            OverloadingPerforator = ItemAPI.Add(new CustomItem(itemdef, displayRules));
            Tuple<ItemIndex, ItemIndex> requirements = new Tuple<ItemIndex, ItemIndex>(ItemIndex.FireballsOnHit, ItemIndex.ChainLightning);
            comboRequirement.Add(OverloadingPerforator, requirements);

            R2API.LanguageAPI.Add("OVERLOADINGPERFORATOR_NAME", "Overloading Perforator");
            R2API.LanguageAPI.Add("OVERLOADINGPERFORATOR_PICKUP", "Chance to fire lightning balls on hit");
            R2API.LanguageAPI.Add("OVERLOADINGPERFORATOR_DESC", "10% chance on hit to call forth 3 lightning balls from an enemy, dealing 300% <style=cStack>(+300% per stack)</style> damage.");
            //R2API.LanguageAPI.Add("OVERLOADINGPERFORATOR_LORE", "");

            RoR2.GlobalEventManager.onServerDamageDealt += lightningballsproc;
        }

        private void lightningballsproc(DamageReport damageReport)
        {
            if (damageReport.attackerBody && damageReport.attackerBody.inventory)
            {
                int itemcount = damageReport.attackerBody.inventory.GetItemCount(OverloadingPerforator);
                if (itemcount > 0 && damageReport.damageInfo.procCoefficient > 0)
                {
                    float chanceToProc = 10 * damageReport.damageInfo.procCoefficient;
                    bool balllss = Util.CheckRoll(chanceToProc, damageReport.attackerMaster);
                    if (balllss)
                    {
                        Vector3 vector2 = damageReport.victimBody.characterMotor ? (damageReport.victim.transform.position + Vector3.up * (damageReport.victimBody.characterMotor.capsuleHeight * 0.5f + 2f)) : (damageReport.victim.transform.position + Vector3.up * 2f);
                        int num8 = 3;
                        float damageCoefficient7 = 3f * (float)itemcount;
                        float damage4 = Util.OnHitProcDamage(damageReport.damageInfo.damage, damageReport.attackerBody.damage, damageCoefficient7);
                        float min = 15f;
                        float max = 30f;
                        ProcChainMask procChainMask8 = damageReport.damageInfo.procChainMask;
                        float speedOverride2 = UnityEngine.Random.Range(min, max);
                        float num9 = (float)(360 / num8);
                        float num11 = 1f;
                        float num12 = num9;
                        Vector3 forward2 = Vector3.up;
                        for (int k = 0; k < num8; k++)
                        {
                            float num13 = (float)k * 3.1415927f * 2f / (float)num8;
                            RoR2.Projectile.FireProjectileInfo fireProjectileInfo = new RoR2.Projectile.FireProjectileInfo
                            {
                                projectilePrefab = Resources.Load<GameObject>("Prefabs/Projectiles/ElectricOrbProjectile"),
                                position = vector2 + new Vector3(num11 * Mathf.Sin(num13), 0f, num11 * Mathf.Cos(num13)),
                                rotation = Util.QuaternionSafeLookRotation(forward2),
                                procChainMask = procChainMask8,
                                target = damageReport.victimBody.gameObject,
                                owner = damageReport.attacker,
                                damage = damage4,
                                crit = damageReport.attackerBody.RollCrit(),
                                force = 200f,
                                damageColorIndex = DamageColorIndex.Item,
                                speedOverride = speedOverride2,
                                useSpeedOverride = true
                            };
                            num12 += num9;
                            RoR2.Projectile.ProjectileManager.instance.FireProjectile(fireProjectileInfo);
                            forward2.x += Mathf.Sin(num13 + UnityEngine.Random.Range(-20, 20));
                            forward2.z += Mathf.Cos(num13 + UnityEngine.Random.Range(-20, 20));
                        }
                    }
                }
            }
        }
        #endregion
        #region CornedBeef
        private void AddCornedBeef()
        {
            ItemDef itemdef = new ItemDef
            {
                name = "CORNEDBEEF_NAME",
                nameToken = "CORNEDBEEF_NAME",
                pickupToken = "CORNEDBEEF_PICKUP",
                descriptionToken = "CORNEDBEEF_DESC",
                //loreToken = "CORNEDBEEF_LORE",
                tier = ItemTier.NoTier,
                pickupIconPath = "Textures/MiscIcons/texMysteryIcon",
                pickupModelPath = "Prefabs/PickupModels/PickupMystery",
                //Can remove determines if a shrine of order, or a printer can take this item, generally true, except for NoTier items.
                canRemove = false,
                //Hidden means that there will be no pickup notification, and it won't appear in the inventory at the top of the screen. This is useful for certain noTier helper items, such as the DrizzlePlayerHelper.
                hidden = false
            };

            var displayRules = new ItemDisplayRuleDict(null);

            CornedBeef = ItemAPI.Add(new CustomItem(itemdef, displayRules));
            Tuple<ItemIndex, ItemIndex> requirements = new Tuple<ItemIndex, ItemIndex>(ItemIndex.Clover, ItemIndex.BleedOnHitAndExplode);
            comboRequirement.Add(CornedBeef, requirements);

            R2API.LanguageAPI.Add("CORNEDBEEF_NAME", "Corned Beef");
            R2API.LanguageAPI.Add("CORNEDBEEF_PICKUP", "Every successful proc applies bleed");
            R2API.LanguageAPI.Add("CORNEDBEEF_DESC", "Every successful proc applies 1 stack of bleed <style=cStack>(+1 per stack)</style>.");
            //R2API.LanguageAPI.Add("CORNEDBEEF_LORE", "");

            RoR2.GlobalEventManager.onServerDamageDealt += cornedbeefproc;
        }

        private void cornedbeefproc(DamageReport damageReport)
        {
            if (damageReport.attackerBody && damageReport.attackerBody.inventory)
            {
                int itemcount = damageReport.attackerBody.inventory.GetItemCount(CornedBeef);
                if (itemcount > 0)
                {
                    int stacksToApply = 0;
                    ProcChainMask procChainMask = damageReport.damageInfo.procChainMask;
                    foreach (ProcType procType in Enum.GetValues(typeof(ProcType)))
                    {
                        if (procChainMask.HasProc(procType))
                        {
                            stacksToApply++;
                        }
                    }
                    for (int i = 0; i < stacksToApply; i++)
                    {
                        DotController.InflictDot(damageReport.victim.gameObject, damageReport.attacker, DotController.DotIndex.Bleed, 3f * damageReport.damageInfo.procCoefficient, 1f);
                    }
                }
            }
        }
        #endregion
    }
}
