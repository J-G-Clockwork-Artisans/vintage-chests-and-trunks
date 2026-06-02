# Changelog

All notable changes to this project will be documented in this file.

## [1.1.0-rc.1] - 2026-06-01

### Added
- **Wildcraft Mod Support:** Added native compatibility for **44 wood variants** from *Wildcraft: Trees and Shrubs* (`wildcrafttree`) and *Wildcraft: Trees and Shrubs (Extended)* (`wildcrafttreeextended`).
- **Dynamic JSON Patching:** Added conditional patching rules that only run if a Wildcraft mod is present. If Wildcraft is not installed, the extra wood types and recipes are completely bypassed, preventing log warnings or empty/untextured blocks in the creative menu.
- **Wildcraft Crafting Recipes:** Added recipes for all 44 Wildcraft wood chests. They follow the same layout as vanilla wood-typed chests (Nails & Strips at the top-center, surrounded by planks of the exact same wood type).
- **Wildcraft Chest & Trunk Translations:** Added English and Portuguese (PT-BR) localizations for all 44 new wood chests and trunks.
- **Wood Variants Included:**
  - Cedar Pine, Fir, Spruce, Douglas Fir, Thuya, Red Cedar, Yew, Kauri, Ginkgo, Sycamore, Honey Locust, Grenadill, Pink Ivory, Banyan, Elm, Beech, Bearnut, Willow, Poplar, Tamanu, Spurge Wood, Azobe, Lignum Vitae, Eucalyptus, Ghost Gum, Ohia, Satinash, Leadwood, Blue Mahoe, Linden, Sal, Chestnut, Tigerwood, Mahogany, Sapele, Saxaul, Ash, Catalpa, Palissandre, Mangrove, Empress Wood, Charred, Chlorociboria-dyed, Petrified.

### Fixed
- **Accessory Textures:** Corrected the accessory texture paths for trunks in the JSON mappings, resolving the console log warnings about missing `accessories.png` assets.

---

## [1.0.0] - 2026-05-31

### Added
- Initial release of the mod.
- Support for 12 vanilla wood types.
- Dynamic texture blending for chests and trunks.
- Basic translations for EN, PT-BR, DE, FR, ES, RU.
