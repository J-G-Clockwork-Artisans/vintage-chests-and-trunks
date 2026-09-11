# Vintage Chests & Trunks: Timber Selection

A Vintage Story content mod that makes chests and trunks visually match the type of wood they were crafted with, using dynamic texture blending to keep mod size extremely lightweight.

## Features

* **12 Vanilla Wood Variants:** Fully supports all vanilla wood types: Oak, Birch, Pine, Redwood, Ebony, Larch, Kapok, Maple, Purpleheart, Walnut, Bald Cypress, and Acacia.
* **44 Wildcraft Wood Variants:** Fully supports all unique wood types from [Wildcraft: Trees and Shrubs](https://mods.vintagestory.at/wildcrafttree) and [Wildcraft: Trees and Shrubs (Extended)](https://mods.vintagestory.at/wildcrafttreeextended).
* **42 Biodiversity Wood Variants:** Fully supports wood types from the [Biodiversity: Trees](https://mods.vintagestory.at/biodiversity) mod.
* **Wood-Typed Labeled Chests & Trunks (New since v1.2.0-rc.3):** Write custom text labels on your containers using Charcoal or Chalk! Perfect for keeping your storage base beautifully organized and identifying resources at a glance, with a seamless, glitch-free typing and click-anywhere editing experience.
* **Vanilla Coexistence:** Does not override or block vanilla chests or trunks; they remain craftable and functional side-by-side with the new wood-typed versions.
* **Full Translation Support:** Localized and translated out of the box for English, Portuguese (PT-BR), German, French, Spanish (Spain & LATAM), and Russian.

## Crafting Recipes

Because vanilla Vintage Story crafting allows mixing different wood types to make a single chest, this mod uses a slightly modified layout to distinguish wood-typed containers from the default ones.

### Wood-Typed Chest
Place **Nails & Strips** in the **top-center** slot of the grid, and fill the remaining slots with **planks of the exact same wood type**.
```
[ Plank ] [ Nails ] [ Plank ]
[ Plank ] [ Plank ] [ Plank ]
[ Plank ] [ Plank ] [ Plank ]
```
*Note: Using the vanilla recipe (Nails in the center slot) or mixing different wood types will still produce a vanilla Wooden Chest.*

### Wood-Typed Trunk
Place **two identical wood-typed chests** side-by-side anywhere in the crafting grid to upgrade them.
```
[ Typed Chest ] [ Typed Chest ]
```
*Note: Mixing different wood-typed chests will result in a vanilla Wooden Trunk.*

### Labeled Chests & Trunks (New in v1.2.0-rc.3)

Upgrade any wood-typed or vanilla container with a sign (shapeless recipes).

#### Wood-Typed Labeled Chest
```
[ Typed Chest ] [ Wooden Sign ]
```

#### Wood-Typed Labeled Trunk (Option A or B)
```
[ Labeled Chest ] [ Labeled Chest ]   OR   [ Typed Trunk ] [ Wooden Sign ]
```
*Note: Both Labeled Chests must be of the exact same wood type.*

#### Vanilla Labeled Trunk (Option A or B)
```
[ Vanilla Labeled ] [ Vanilla Labeled ]   OR   [ Vanilla Trunk ] [ Wooden Sign ]
```
*Note: Maintains the vanilla texture blending and is generically named "Labeled Trunk".*

## Note about the development of this mod

Some of the code in this mod was written with the help of AI tools, but this is far from a "vibe-coded" project. As a developer, I have invested hours of my own life orchestrating, analyzing, and directing this implementation. Every line of code, recipe modification, and asset patch was carefully designed, verified, and heavily tested in-game to meet the high standards of how this mod should look, feel, and behave. 

This process has been an incredible learning experience for me. I am constantly diving deep into Vintage Story's modding patterns, and I am completely open to feedback! If you are a more experienced developer, please feel free to inspect the codebase directly on GitHub and suggest improvements or best practices. I have a completely open mind and would love to hear your suggestions.

## Inspiration & Credits
* **Inspiration:** This mod was inspired by MIghtyGooga's excellent **mightychests** that makes wonders with stone typed chests.
* **Co-Author:** MIghtyGooga is a co-author of this mod.
* **Compatibility Request:** Special thanks to user **@Jhoulana** (on our [ModDB Page](https://mods.vintagestory.at/vintagechestsandtrunks)) for suggesting and requesting compatibility with the Wildcraft mod series!
* **Compatibility Request:** Special thanks to user **@DeeBz26** for requesting compatibility with the Biodiversity mod series!
* **Feature Suggestion:** Special thanks to user **@SunriderKSY** for suggesting the implementation of labeled chests and trunks!
* **License:** GNU General Public License v3.0 (GPLv3)
