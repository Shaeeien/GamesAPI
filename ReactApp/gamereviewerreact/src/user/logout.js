import React, { useContext } from "react";
import UserContext from "./userContext";
import '../layout/index.css';

export default function Logout(){    
    const user = useContext(UserContext);
    user.email = "";
    return <div id="content">
            Wylogowano!
        </div>
}