import React from "react";
import { NavLink } from "react-router-dom";
import { useContext } from "react";
import UserContext from "../user/userContext";

export default function LoggedInNavbar(props){         
    return (
        <ul className="navbar">
            <li className="navbar-li">
                <NavLink to="/">
                    Strona główna
                </NavLink>
            </li>
            <li className="navbar-li">
                <NavLink to="/games">
                    Encyklopedia gier
                </NavLink>
            </li>
            <li className="navbar-li">
                Zalogowano { props.user.email }
            </li>
            <li className="navbar-li">
                <NavLink to="/logout">
                    Wyloguj się
                </NavLink>
            </li>                     
        </ul>                                           
    );
}