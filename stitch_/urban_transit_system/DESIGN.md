---
name: Urban Transit System
colors:
  surface: '#121414'
  surface-dim: '#121414'
  surface-bright: '#393939'
  surface-container-lowest: '#0d0e0f'
  surface-container-low: '#1b1c1c'
  surface-container: '#1f2020'
  surface-container-high: '#292a2a'
  surface-container-highest: '#343535'
  on-surface: '#e3e2e2'
  on-surface-variant: '#c6c6c6'
  inverse-surface: '#e3e2e2'
  inverse-on-surface: '#303031'
  outline: '#919191'
  outline-variant: '#474747'
  surface-tint: '#ffb599'
  primary: '#ffb599'
  on-primary: '#5a1c00'
  primary-container: '#7b2f0a'
  on-primary-container: '#ffdbce'
  inverse-primary: '#9a4520'
  secondary: '#e7bdae'
  on-secondary: '#442a20'
  secondary-container: '#5d4034'
  on-secondary-container: '#ffdbce'
  tertiary: '#d4c78e'
  on-tertiary: '#383006'
  tertiary-container: '#50471b'
  on-tertiary-container: '#f1e3a8'
  error: '#ffb4ab'
  on-error: '#690005'
  error-container: '#93000a'
  on-error-container: '#ffdad6'
  primary-fixed: '#ffdbce'
  primary-fixed-dim: '#ffb599'
  on-primary-fixed: '#370e00'
  on-primary-fixed-variant: '#7b2f0a'
  secondary-fixed: '#ffdbce'
  secondary-fixed-dim: '#e7bdae'
  on-secondary-fixed: '#2c160c'
  on-secondary-fixed-variant: '#5d4034'
  tertiary-fixed: '#f1e3a8'
  tertiary-fixed-dim: '#d4c78e'
  on-tertiary-fixed: '#211b00'
  on-tertiary-fixed-variant: '#50471b'
  background: '#121414'
  on-background: '#e3e2e2'
  surface-variant: '#343535'
typography:
  headline-xl:
    fontFamily: Plus Jakarta Sans
    fontSize: 40px
    fontWeight: '700'
    lineHeight: 48px
    letterSpacing: -0.02em
  headline-lg:
    fontFamily: Plus Jakarta Sans
    fontSize: 32px
    fontWeight: '700'
    lineHeight: 40px
    letterSpacing: -0.02em
  headline-md:
    fontFamily: Plus Jakarta Sans
    fontSize: 24px
    fontWeight: '600'
    lineHeight: 32px
  body-lg:
    fontFamily: Plus Jakarta Sans
    fontSize: 18px
    fontWeight: '400'
    lineHeight: 28px
  body-md:
    fontFamily: Plus Jakarta Sans
    fontSize: 16px
    fontWeight: '400'
    lineHeight: 24px
  label-lg:
    fontFamily: Plus Jakarta Sans
    fontSize: 14px
    fontWeight: '600'
    lineHeight: 20px
    letterSpacing: 0.01em
  label-sm:
    fontFamily: Plus Jakarta Sans
    fontSize: 12px
    fontWeight: '500'
    lineHeight: 16px
    letterSpacing: 0.02em
rounded:
  sm: 0.25rem
  DEFAULT: 0.5rem
  md: 0.75rem
  lg: 1rem
  xl: 1.5rem
  full: 9999px
spacing:
  base: 8px
  xs: 4px
  sm: 12px
  md: 24px
  lg: 40px
  xl: 64px
  gutter: 16px
  margin-mobile: 16px
  margin-desktop: 32px
---

## Brand & Style

The Urban Transit System is anchored in a **Corporate Modern** aesthetic, now evolved into a sophisticated **Dark Mode** environment. It balances the urgency of public transit with a muted, professional technicality. The goal is to provide a focused, low-strain navigation experience for daily commuters and visitors alike, particularly effective in urban settings.

The style emphasizes high legibility and functional clarity. It avoids excessive decoration in favor of a clear information hierarchy, utilizing subtle depth and soft roundedness to feel approachable rather than clinical. The transition to a "Rainbow" variant introduces a broader spectrum of earth-toned accents, providing a grounding backdrop to the high-visibility primary action colors.

## Colors

This design system uses a dark-themed palette optimized for visibility and reduced eye strain:

- **Primary (Terracotta Orange):** Reserved for critical actions like "Search" buttons, active route highlights, and primary alerts.
- **Secondary (Muted Taupe):** Used for supporting UI elements, secondary information, and less urgent functional markers.
- **Tertiary (Olive Gold):** Provides a distinct accent for specialized transport modes or status indicators.
- **Backgrounds:** The interface utilizes deep, dark surfaces (#1A110E) for the main canvas. Containers and sidebars should utilize tonal elevation to create a premium, high-contrast look when presenting search results or maps.

## Typography

The design system utilizes **Plus Jakarta Sans** across all interfaces. This font was chosen for its modern, clean geometry and slightly rounded terminals, which provide a friendly and approachable feel without sacrificing professional rigor.

Headlines should use the heavier weights (700) and tighter letter spacing to create a sense of urgency and importance for stop names and route numbers. Body text is kept spacious to ensure it remains readable even on small mobile screens or in high-glare environments.

## Layout & Spacing

The design system follows a **Fluid Grid** model with a base-8 spacing scale. This ensures consistency and mathematical harmony across all components.

- **Desktop:** 12-column grid with 24px gutters. Content is typically centered with a max-width of 1280px.
- **Mobile:** Single column with 16px side margins.
- **Rhythm:** Use "md" (24px) for spacing between major sections and "sm" (12px) for internal component padding. This creates a clear visual grouping of transport data.

## Elevation & Depth

In Dark Mode, depth is communicated through **Tonal Layers** rather than heavy shadows to maintain clarity:

- **Level 0 (Base):** Deepest neutral background.
- **Level 1 (Cards):** Slightly lighter surface-container backgrounds to create a subtle lift.
- **Level 2 (Active Elements):** For search bars or interactive transport items, use tonal highlighting or subtle 1px outlines in the Secondary color.
- **Overlays:** Modals and bottom sheets use a backdrop blur to maintain spatial context while focusing the user on the navigation task.

## Shapes

The design system uses a **Rounded** shape language to reinforce the friendly and modern brand personality.

- **Standard Elements:** 8px (0.5rem) radius for buttons and input fields.
- **Cards & Search Bars:** 16px (1rem) radius for a softer, more contemporary container feel.
- **Transport Icons/Tags:** Pill-shaped (fully rounded) for transport mode indicators (e.g., "Bus 42") to make them instantly recognizable as distinct tokens.

## Components

### Search Bar
The primary search interface should be a large, Level-2 elevated container. Use a clear "Start" icon and a "Destination" icon connected by a subtle vertical dashed line. The background should be a raised surface-container color, with a Primary Terracotta action button.

### Transport Mode Icons
- **Bus:** Icon within a Secondary Taupe pill.
- **Trolleybus:** Icon within a Tertiary Olive pill.
- **Minibus:** Icon within a neutral-tinted pill.
Each icon should be accompanied by the route number in bold typography.

### Buttons
- **Primary:** Terracotta Orange background, high-contrast text. High-visibility and easily tappable.
- **Secondary:** Secondary Taupe outline with 2px weight, Secondary text.
- **Ghost:** No background, Primary or Secondary text, used for "Cancel" or "View Details".

### List Items (Routes & Stops)
List items must be highly legible. Each item should have a 12px vertical padding. Use the on-surface color for the stop name (Headline-MD) and Tertiary Olive for arrival times. Include a clear visual indicator (line and dot) for the "track" to show the progression of the route.

### Chips
Used for filtering (e.g., "Fastest," "Fewest Transfers"). When inactive, use a dark surface-variant background. When active, transition to a Primary Terracotta or Secondary Taupe background with high-contrast text.