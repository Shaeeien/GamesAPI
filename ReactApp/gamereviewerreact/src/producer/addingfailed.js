import React from 'react';
import '../layout/index.css';
import { useNavigate } from 'react-router-dom';

export default function AddingFailed(){
    const navigate = useNavigate();

    function Back(){
        navigate("/");
    }

    return <div className='content'>
        <p>Nie udało się dodać producenta</p>
        <input type="button" value="Strona główna" onClick={Back} />
    </div>
}