import React from 'react';
import '../layout/index.css';
import { useNavigate } from 'react-router-dom';

export default function AddingSuccess(){
    const navigate = useNavigate();

    function Back(){
        navigate("/");
    }

    return <div className='content'>
        <p>Dodano wydawcę!</p>
        <input type="button" value="Strona główna" onClick={Back} />
    </div>
}