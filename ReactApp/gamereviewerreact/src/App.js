import React, { useEffect } from "react";
import { Navbar } from './layout/navbar';
import { Footer } from './layout/footer';
import { Sidebar} from './layout/sidebar';
import { Route, BrowserRouter as Router, Routes } from 'react-router-dom';
import Home from "./layout/home";
import Register from "./user/register";
import Games from "./layout/games";
import Login from "./user/login";
import Logout from "./user/logout";
import { useContext } from "react";
import { UserContext } from "./user/userContext.js";
import LoggedInNavbar from "./layout/loggedinnavbar";
import AddGame from "./game/addgame";
import AddProducer from "./producer/addproducer";
import AddingFailed from "./producer/addingfailed";
import AddingSuccess from "./producer/addingsuccess";

export default function App(){     
    const user = useContext(UserContext);

    return <div className="App">
                {user.email !== "" ? <Navbar /> : <LoggedInNavbar user={user}/>}                
                <Sidebar />    
                <Routes>
                    <Route path="/" element={<Home />} />
                    <Route path="/games" element={<Games />} />
                    <Route path="/register" element={<Register />}/>
                    <Route path="/login" element={<Login />} />
                    <Route path="/logout" element={<Logout/>} />
                    <Route path="/games/add" element={<AddGame />} />
                    <Route path="/producers/add" element={<AddProducer />} />
                    <Route path="/producers/add/success" element={<AddingSuccess />} />
                    <Route path="/producers/add/failure" element={<AddingFailed />} />

                </Routes>
                        
                
                
                <Footer></Footer>                         
            </div>
}
    