namespace Eco.Mods.TechTree
{
    // [DoNotLocalize]
}
// WORLD LAYER INFO
namespace Eco.Mods.WorldLayers
{
    using Eco.Shared.Localization;
    using Eco.Shared.Math;
    using Eco.Shared.Utils;
    using Eco.Simulation.WorldLayers.Layers;

    public partial class PlantLayerSettingsAgave : PlantLayerSettings
    {
        public PlantLayerSettingsAgave() : base()
        {
            this.Name = "Agave";
            this.DisplayName = string.Format("{0} {1}", Localizer.DoStr("Agave"), Localizer.DoStr("Population"));
            this.InitMultiplier = 1;
            this.SyncToClient = false;
            this.Range = new Range(0f, 1f);
            this.RenderRange = new Range(0f, 0.333333f);
            this.MinColor = new Color(1f, 1f, 1f);
            this.MaxColor = new Color(0f, 1f, 0f);
            this.Percent = false;
            this.SumRelevant = true;
            this.Unit = "Agave";
            this.VoxelsPerEntry = 5;
            this.Category = WorldLayerCategory.Plant;
            this.ValueType = WorldLayerValueType.Percent;
            this.AreaDescription = "";
        }
    }
}

namespace Eco.Mods.Organisms
{
    using System.Collections.Generic; 
    using Eco.Gameplay.Plants;
    using Eco.Mods.TechTree;
    using Eco.Shared.Localization;
    using Eco.Shared.Math;
    using Eco.Shared.Serialization;
    using Eco.Simulation;
    using Eco.Simulation.Types;
    using Eco.World.Blocks;

    [Serialized]
    public partial class Agave : PlantEntity
    {
        public Agave(WorldPosition3i mapPos, PlantPack plantPack) : base(species, mapPos, plantPack) { }
        public Agave() { }
        static PlantSpecies species;
        public partial class AgaveSpecies : BalancedPlantSpecies
        {
            public AgaveSpecies() : base()
            {
                species = this;
                this.InstanceType = typeof(Agave);

                // Info
                this.Decorative = false;
                this.Name = "Agave";
                this.DisplayName = Localizer.DoStr("Agave");
                // Lifetime
                this.MaturityAgeDays = 0.8f;
                // Generation
                // Food
                this.CalorieValue = 4;
                // Resources
                this.PostHarvestingGrowth = 0;
                this.PickableAtPercent = 0;
                this.ResourceList = new List<SpeciesResource>()
                {
                    new SpeciesResource(typeof(AgaveLeavesItem), new Range(2, 4), 1),
                   new SpeciesResource(typeof(AgaveSeedItem), new Range(1, 2), 0.3f)
                };
                this.ResourceBonusAtGrowth = 0.9f;
                // Visuals
                this.BlockType = typeof(AgaveBlock);
                // Climate
                this.ReleasesCO2ppmPerDay = -0.00001f;
                // WorldLayers
                this.MaxGrowthRate = 0.01f;
                this.MaxDeathRate = 0.005f;
                this.SpreadRate = 0.001f;
                this.ResourceConstraints.Add(new ResourceConstraint() { LayerName = "Phosphorus", HalfSpeedConcentration =  0.1f, MaxResourceContent =  0.2f });
                this.ResourceConstraints.Add(new ResourceConstraint() { LayerName = "SoilMoisture", HalfSpeedConcentration =  0.1f, MaxResourceContent =  0.05f }); 
                this.CapacityConstraints.Add(new CapacityConstraint() { CapacityLayerName = "FertileGround", ConsumedCapacityPerPop =  1 });
                this.CapacityConstraints.Add(new CapacityConstraint() { CapacityLayerName = "ShrubSpace", ConsumedCapacityPerPop =  3 }); 
                this.GenerationSpawnCountPerPoint = new Range(5, 11); 
                this.GenerationSpawnPointMultiplier = 0.1f; 
                this.IdealTemperatureRange = new Range(0.75f, 0.85f);
                this.IdealMoistureRange = new Range(0.1f, 0.25f);
                this.IdealWaterRange = new Range(0, 0.1f);
                this.WaterExtremes = new Range(0, 0.2f);
                this.TemperatureExtremes = new Range(0.7f, 1);
                this.MoistureExtremes = new Range(0, 0.35f);
                this.MaxPollutionDensity = 0.7f;
                this.PollutionDensityTolerance = 0.1f;
                this.VoxelsPerEntry = 5;

            }
        }
    }
    [Serialized]
    [Reapable] 
    [MoveEfficiency(0.5f)] 
    public partial class AgaveBlock :
        PlantBlock { } 
}
