import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react'
import federation from '@originjs/vite-plugin-federation'

// https://vitejs.dev/config/
export default defineConfig({
  base: "./",
  plugins: [react(),
    federation({
      name: "remoteApp3",
      filename: "remoteEntry3.js",
      exposes: {
        "./CustomHeading3": "./src/CustomHeading3",
      },
      shared: ["react", "react-dom"]
    }),
  ],
    server:{
      port: 3003,
    },
    build:{
      target: "esnext",
      minify:false,
      cssCodeSplit: false,
    },
})
