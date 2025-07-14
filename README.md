# Procedural Terrain + Foliage Generator

A Unity-based tool for procedurally generating terrain and foliage using Perlin noise, optimized for strategy game development. Inspired by tutorials from Sebastian Lague, this project focuses on creating natural-looking terrain with water, grass, mountains, and clustered forests.

## Overview

This generator creates:
- A **height map** using Perlin noise to define terrain elevation
- A **texture map** to distinguish terrain zones (e.g., water, grassland, mountains)
- **Forest chunks** generated using a secondary noise map
- **Grass foliage** spread broadly across the terrain

Tree generation uses Unity’s built-in terrain tools combined with Level of Detail (LOD) systems to maintain performance at high densities.

## Features

- Procedural height and texture maps
- Clustered forest generation using a secondary noise layer
- `treeChunkSize` variable to control forest patch sizes
- Adjustable tree density (supports up to ~15,000 trees with good performance)
- Grass rendering with broad coverage
- Unity LOD integration for smooth transitions

## Key Component: `ObjectGenerator.cs`

The core logic resides in the `ObjectGenerator` script. It:
- Uses a secondary Perlin noise map to place trees in forest-like clusters
- Ensures trees are placed only in valid elevation zones (not underwater or on peaks)
- Allows for customization of forest size, density, and spread via script parameters

## Performance Notes

- Tree count scales well up to ~15,000 instances before performance issues occur (tested on a mid-range machine)
- LOD ensures smooth rendering even with large forests
- Grass is lightweight and spread across the full terrain surface

## References

- Based on [Sebastian Lague’s Procedural Terrain Generation Tutorial](https://www.youtube.com/playlist?list=PLFt_AvWsXl0f0hqkvuVnWgF3b8MUZ60gz)

## Getting Started

1. Clone this repository into your Unity project:
   ```bash
   git clone https://github.com/elijahtab/Real-Time-Strategy-Demo.git
