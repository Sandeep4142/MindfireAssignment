import { defineConfig } from 'vite';
import react from '@vitejs/plugin-react';
import federation from '@originjs/vite-plugin-federation';

// https://vitejs.dev/config/
export default defineConfig({
  base: "http://localhost:3001/",
  plugins: [
    react(),
    federation({
      name: "remoteApp1",
      filename: "remoteEntry1.js",
      exposes: {
        "./CustomHeading": "./src/CustomHeading",
      },
      shared: ["react", "react-dom"]
    }),
  ],
  server: {
    port: 3001,
  },
  build: {
    target: "esnext",
    minify: false,
    cssCodeSplit: false,
  },
});
