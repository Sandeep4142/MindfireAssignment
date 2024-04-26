import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react'
import federation from '@originjs/vite-plugin-federation'

// https://vitejs.dev/config/
export default defineConfig({
  base: "./",
  plugins: [react(),
    federation({
      name: "remoteApp2",
      filename: "remoteEntry2.js",
      exposes: {
        "./CustomHeading2": "./src/CustomHeading2",
      },
      shared: ["react", "react-dom"]
    }),
  ],
    server:{
      port: 3002,
    },
    build:{
      target: "esnext",
      minify:false,
      cssCodeSplit: false,
    },
})
