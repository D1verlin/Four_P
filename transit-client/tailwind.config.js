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
        "secondary-container": "#ffdbce",
        "on-secondary-fixed-variant": "#5d4034",
        "on-secondary": "#ffffff",
        "inverse-surface": "#392e2a",
        "error": "#ba1a1a",
        "on-background": "#231a17",
        "primary": "#9a4520",
        "on-primary-container": "#370e00",
        "surface": "#fff8f6",
        "on-primary": "#ffffff",
        "inverse-on-surface": "#ffede7",
        "outline": "#85736d",
        "primary-container": "#ffdbce",
        "on-primary-fixed-variant": "#7b2f0a",
        "on-tertiary-container": "#211b00",
        "error-container": "#ffdad6",
        "surface-variant": "#f5d5c9",
        "on-tertiary-fixed-variant": "#50471b",
        "surface-container": "#ffeae3",
        "surface-container-high": "#ffe4dc",
        "secondary": "#77574b",
        "surface-tint": "#9a4520",
        "on-surface": "#231a17",
        "on-surface-variant": "#53433e",
        "tertiary-fixed": "#f1e3a8",
        "primary-fixed": "#ffdbce",
        "outline-variant": "#d8b9ae",
        "on-primary-fixed": "#370e00",
        "tertiary-fixed-dim": "#d4c78e",
        "surface-container-low": "#fff1eb",
        "primary-fixed-dim": "#ffb599",
        "on-error": "#ffffff",
        "surface-dim": "#e8d6d0",
        "secondary-fixed": "#ffdbce",
        "on-secondary-container": "#2c160c",
        "on-error-container": "#410002",
        "surface-bright": "#fff8f6",
        "on-tertiary-fixed": "#211b00",
        "on-secondary-fixed": "#2c160c",
        "on-tertiary": "#ffffff",
        "tertiary-container": "#f1e3a8",
        "background": "#fff8f6",
        "surface-container-highest": "#ffddd4",
        "tertiary": "#695f30",
        "surface-container-lowest": "#ffffff",
        "secondary-fixed-dim": "#e7bdae",
        "inverse-primary": "#ffb599"
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
