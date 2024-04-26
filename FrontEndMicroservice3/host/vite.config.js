import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react'
import federation from '@originjs/vite-plugin-federation'

// https://vitejs.dev/config/
export default defineConfig({
  plugins: [react(),
    federation({
      name: 'host-app',
      remotes:{
        remoteApp1: "http://localhost:3001/assets/remoteEntry1.js",
        remoteApp2: "http://localhost:3002/assets/remoteEntry2.js",
        remoteApp3: "http://localhost:3003/assets/remoteEntry3.js",
      },
      shared: ["react", "react-dom"]
    })
  ],
  server:{
    port:5001,
  }
})
