import React from 'react';
import { NavLink } from 'react-router-dom';

function Sidebar(){
    return <div id="mySidenav" className="sidenav">
                <NavLink to="about">About</NavLink>
                <NavLink to="services">Services</NavLink>
                <NavLink to="clients">Clients</NavLink>
                <NavLink to="contact">Contact</NavLink>
            </div> 
}

export {Sidebar}