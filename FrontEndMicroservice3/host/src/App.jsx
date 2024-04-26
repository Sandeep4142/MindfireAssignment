import { useState } from 'react'
import reactLogo from './assets/react.svg'
import viteLogo from '/vite.svg'
import './App.css'
import CustomHeading from 'remoteApp1/CustomHeading'
import CustomHeading2 from 'remoteApp2/CustomHeading2'
import CustomHeading3 from 'remoteApp3/CustomHeading3'

function App() {
  const [count, setCount] = useState(0)

  return (
    <>
      <h1>Host Container</h1>
      <CustomHeading />
      <CustomHeading2 />
      <CustomHeading3 />
    </>
  )
}

export default App
