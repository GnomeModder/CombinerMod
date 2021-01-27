using System;
using System.Collections.Generic;
using System.Text;
using BepInEx;
using RoR2;
using R2API;
using R2API.Utils;
using UnityEngine;
using System.Linq;

namespace Gnome.CombinerMod
{
    [R2APISubmoduleDependency("ResourcesAPI")]
    [R2APISubmoduleDependency("AssetAPI")]
    [R2APISubmoduleDependency("ItemAPI")]
    [R2APISubmoduleDependency("ItemDropAPI")]
    [R2APISubmoduleDependency("LanguageAPI")]
    [R2APISubmoduleDependency("BuffAPI")]
    [BepInDependency("com.bepis.r2api")]
    [BepInPlugin("com.Gnome.coMbinerMod",
        "coMbinerMod",
        "0.0.1")]
    public class coMbinerMod : BaseUnityPlugin
    {
        public ComboItems Items;
        public void Awake()
        {
            AssetBundleResourcesProvider provider = new AssetBundleResourcesProvider("@Combiner", Assets.combinerAssetBundle);
            ResourcesAPI.AddProvider(provider);
            Items = new ComboItems();
            Items.AddAllItems();
        }

        bool checkingInput = false;
        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.F1))
            {
                PlayerCharacterMasterController[] pcmc = new PlayerCharacterMasterController[1];
                PlayerCharacterMasterController.instances.CopyTo(pcmc, 0);
                GiveItem(Items.GetRandomPossibleCombo(pcmc[0].master));

                Vector3 pos = pcmc[0].master.GetBody().footPosition;
                Chat.AddMessage(pos.ToString());
            }
            if (Input.GetKeyDown(KeyCode.Equals))
            {
                checkingInput = true;
            }
            if (checkingInput)
            {
                if (Input.GetKeyDown(KeyCode.A))
                {
                    GiveItem(Items.CarnivorousSlug);
                }
                if (Input.GetKeyDown(KeyCode.B))
                {
                    GiveItem(Items.ImprovisedWeapon);
                }
                if (Input.GetKeyDown(KeyCode.C))
                {
                    GiveItem(Items.NapalmRounds);
                }
                if (Input.GetKeyDown(KeyCode.D))
                {
                    GiveItem(Items.AncestralNecklace);
                }
                if (Input.GetKeyDown(KeyCode.E))
                {
                    GiveItem(Items.BloodsuckingFruit);
                }
                if (Input.GetKeyDown(KeyCode.F))
                {
                    GiveItem(Items.Bootlegs);
                }
                if (Input.GetKeyDown(KeyCode.G))
                {
                    GiveItem(Items.CrimsonBison);
                }
                if (Input.GetKeyDown(KeyCode.H))
                {
                    //GiveItem(Items.ScorchedEarth);
                }
                if (Input.GetKeyDown(KeyCode.I))
                {
                    GiveItem(Items.MustardGas);
                }
                if (Input.GetKeyDown(KeyCode.J))
                {
                    GiveItem(Items.CyberneticInfusion);
                    GiveItem(ItemIndex.Infusion);
                }
                if (Input.GetKeyDown(KeyCode.K))
                {
                    GiveItem(Items.RosePlating);
                }
                if (Input.GetKeyDown(KeyCode.L))
                {
                    GiveItem(Items.TheManifesto);
                }
                if (Input.GetKeyDown(KeyCode.M))
                {
                    GiveItem(Items.BirdsOfAFeather);
                }
                if (Input.GetKeyDown(KeyCode.N))
                {
                    GiveItem(Items.TheSpark);
                }
                if (Input.GetKeyDown(KeyCode.O))
                {
                    GiveItem(Items.InvisiblityCloak);
                }
                if (Input.GetKeyDown(KeyCode.P))
                {
                    //GiveItem(Items.TheCollectiveUnconscious);
                }
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    GiveItem(Items.CrownOfFrost);
                }
                if (Input.GetKeyDown(KeyCode.R))
                {
                    GiveItem(Items.LuckyKey);
                }
                if (Input.GetKeyDown(KeyCode.S))
                {
                    GiveItem(Items.TheHardStuff);
                }
                if (Input.GetKeyDown(KeyCode.T))
                {
                    GiveItem(Items.BioFuelCell);
                }
                if (Input.GetKeyDown(KeyCode.U))
                {
                    GiveItem(Items.CrownOfEmbers);
                }
                if (Input.GetKeyDown(KeyCode.V))
                {
                    GiveItem(Items.AnniversaryGift);
                }
                if (Input.GetKeyDown(KeyCode.W))
                {
                    GiveItem(Items.CrownOfThunder);
                }
                if (Input.GetKeyDown(KeyCode.X))
                {
                    GiveItem(Items.LoyalSoldiers);
                }
                if (Input.GetKeyDown(KeyCode.Y))
                {
                    GiveItem(Items.OverloadingPerforator);
                }
                if (Input.GetKeyDown(KeyCode.Z))
                {
                    GiveItem(Items.CornedBeef);
                }
            }
        }

        private void GiveItem(ItemIndex item)
        {
            var transform = PlayerCharacterMasterController.instances[0].master.GetBodyObject().transform;
            PickupDropletController.CreatePickupDroplet(PickupCatalog.FindPickupIndex(item), transform.position, transform.forward * 20f);
            checkingInput = false;
        }

        private void makeCombo()
        {

        }
    }
}