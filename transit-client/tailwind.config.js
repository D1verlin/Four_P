/** @type {import('tailwindcss').Config} */
export default {
  content: [
    "./index.html",
    "./src/**/*.{js,ts,jsx,tsx}",
  ],
  darkMode: "class",
  theme: {
    extend: {
      colors: {
        "secondary-container": "#5d4034",
        "on-secondary-fixed-variant": "#5d4034",
        "on-secondary": "#442a20",
        "inverse-surface": "#e3e2e2",
        "error": "#ffb4ab",
        "on-background": "#e3e2e2",
        "primary": "#ffb599",
        "on-primary-container": "#ffdbce",
        "surface": "#121414",
        "on-primary": "#5a1c00",
        "inverse-on-surface": "#303031",
        "outline": "#919191",
        "primary-container": "#7b2f0a",
        "on-primary-fixed-variant": "#7b2f0a",
        "on-tertiary-container": "#f1e3a8",
        "error-container": "#93000a",
        "surface-variant": "#343535",
        "on-tertiary-fixed-variant": "#50471b",
        "surface-container": "#1f2020",
        "surface-container-high": "#292a2a",
        "secondary": "#e7bdae",
        "surface-tint": "#ffb599",
        "on-surface": "#e3e2e2",
        "on-surface-variant": "#c6c6c6",
        "tertiary-fixed": "#f1e3a8",
        "primary-fixed": "#ffdbce",
        "outline-variant": "#474747",
        "on-primary-fixed": "#370e00",
        "tertiary-fixed-dim": "#d4c78e",
        "surface-container-low": "#1b1c1c",
        "primary-fixed-dim": "#ffb599",
        "on-error": "#690005",
        "surface-dim": "#121414",
        "secondary-fixed": "#ffdbce",
        "on-secondary-container": "#ffdbce",
        "on-error-container": "#ffdad6",
        "surface-bright": "#393939",
        "on-tertiary-fixed": "#211b00",
        "on-secondary-fixed": "#2c160c",
        "on-tertiary": "#383006",
        "tertiary-container": "#50471b",
        "background": "#121414",
        "surface-container-highest": "#343535",
        "tertiary": "#d4c78e",
        "surface-container-lowest": "#0d0e0f",
        "secondary-fixed-dim": "#e7bdae",
        "inverse-primary": "#9a4520"
      },
      borderRadius: {
        DEFAULT: "0.25rem",
        lg: "0.5rem",
        xl: "0.75rem",
        full: "9999px"
      },
      spacing: {
        xl: "64px", base: "8px", gutter: "16px",
        md: "24px", lg: "40px", sm: "12px",
        "margin-desktop": "32px", xs: "4px", "margin-mobile": "16px"
      },
      fontFamily: {
        sans: ["Inter", "sans-serif"],
        "body-md": ["Inter", "sans-serif"],
        "headline-lg": ["Inter", "sans-serif"],
        "headline-md": ["Inter", "sans-serif"],
        "body-lg": ["Inter", "sans-serif"],
        "label-sm": ["Inter", "sans-serif"],
        "headline-xl": ["Inter", "sans-serif"],
        "label-lg": ["Inter", "sans-serif"]
      },
      fontSize: {
        "body-md": ["16px", { lineHeight: "24px", fontWeight: "400" }],
        "headline-lg": ["32px", { lineHeight: "40px", letterSpacing: "-0.02em", fontWeight: "700" }],
        "headline-md": ["24px", { lineHeight: "32px", fontWeight: "600" }],
        "body-lg": ["18px", { lineHeight: "28px", fontWeight: "400" }],
        "label-sm": ["12px", { lineHeight: "16px", letterSpacing: "0.02em", fontWeight: "500" }],
        "headline-xl": ["40px", { lineHeight: "48px", letterSpacing: "-0.02em", fontWeight: "700" }],
        "label-lg": ["14px", { lineHeight: "20px", letterSpacing: "0.01em", fontWeight: "600" }]
      }
    }
  },
  plugins: [
    require('@tailwindcss/forms'),
    require('@tailwindcss/container-queries')
  ],
}
