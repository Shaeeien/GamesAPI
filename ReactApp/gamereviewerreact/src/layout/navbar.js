import React, { useContext, useEffect, useState } from 'react';
import ReactDOM from 'react-dom';
import './index.css';
import { useLocation, useNavigate } from 'react-router-dom';
import { NavLink } from 'react-router-dom';
import Home from './home';
import UserContext from '../user/userContext';

export function Navbar(props){   
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
                <NavLink to="/register">
                    Zarejestruj się
                </NavLink>
            </li>
            <li className="navbar-li">
                <NavLink to="/login">
                    Zaloguj się
                </NavLink>
            </li>                     
        </ul>                                           
    );
}
        


