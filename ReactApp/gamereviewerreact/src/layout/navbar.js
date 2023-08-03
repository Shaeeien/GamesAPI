import React, { useState } from 'react';

import './index.css';
import { NavLink, Navigate} from 'react-router-dom';
import { useEffect, useRef } from 'react';

export function Navbar(props) {

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
            {
                    props.loggedIn ?
                    <li className="navbar-li">
                        <NavLink onClick={props.Logout}>
                            Wyloguj się
                        </NavLink>
                    </li> :
                    <div>
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
                    </div>
            }                       
    </ul>                                           
    );
}
        


