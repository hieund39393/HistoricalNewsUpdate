import { Routes, Route, BrowserRouter } from "react-router-dom"
import Home from "./pages/Home"
import Layout from "./components/Layout";
import Login from "./pages/login/Login";

function App() {
 return (
   <div className="App">
     <BrowserRouter>
       <Routes>
         <Route element={<Layout />} >
           <Route path="/" element={<Home />} />
         </Route>
         <Route element={<Login />} path="login" />
       </Routes>
     </BrowserRouter>
   </div>
 )
}

export default App