import React from 'react';
import { Outlet, Navigate } from 'react-router-dom'; 

export default function ProtectedRoute() {
    if (localStorage.getItem("email") != null)
        return <Outlet />
    else return <Navigate to="/login"/>
}