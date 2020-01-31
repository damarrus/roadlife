using MadaoEcs;
using RaidHealer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EcsTestBehaviour1 : MonoBehaviour {

    public void LoadConfigs() {

        // PLAYER
        
        //EcsConfigUtil.AddConfig("helmet-2-hero-power-stat-buff", @"TargetTypeFullName: RaidHealer.Templates.HeroPowerStatBuffTemplate" + System.Environment.NewLine + NTestItems.Helmet_2_HeroPowerBuff());
    }

    public void CreateTestInitialEntities() {
        //var user = EcsConfigUtil.CreateEntity("user");
        //user.AddComponent(new SelfComponent());
        //var userGroup = user.GetIComponent<UserGroupComponent>(UserGroupComponent.TypeId);
        //var gold = EcsConfigUtil.CreateEntity("gold");
        //gold.GetIComponent<CountComponent>(CountComponent.TypeId).Count = 500;
        //var hard = EcsConfigUtil.CreateEntity("hardCurrency");

        //gold.SendEvent(new GiveEvent(userGroup, user));
        //hard.SendEvent(new GiveEvent(userGroup, user));

        //var priestHero = EcsConfigUtil.CreateEntity("priest-hero");
        //priestHero.AddComponent(new SelectedComponent());
        //priestHero.AddComponent(new SelfComponent());

        //var helmet1 = EcsConfigUtil.CreateEntity("helmet_1");
        //var heroGroup = priestHero.GetIComponent<UnitGroupComponent>(UnitGroupComponent.TypeId);
        //helmet1.SendEvent(new GiveEvent(heroGroup, priestHero));
        //helmet1.SendEvent(new EquipEvent(heroGroup));

        //helmet1.SendEvent(new UpgradeItemEvent());
    }
}
