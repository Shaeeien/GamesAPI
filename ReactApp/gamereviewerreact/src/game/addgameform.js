import React from "react";
import { useState } from "react";
import axios from "axios";
import '../layout/index.css';

export default function AddGameForm(){
    const[gameName, setGameName] = useState('');
    const[description, setDescription] = useState('');
    const[avgPlayTime, setAvgPlayTime] = useState(0.0);
    const[cover, setCover] = useState(null);
    const[images, setImages] = useState([]);

    const HandleSubmit = async(event) => {
        event.preventDefault();
        console.log(cover);
        console.log(images);   
        const formData = new FormData();
        formData.append(images);
        if(gameName !== '' && description !== '' && avgPlayTime > 0.0
        && cover !== null && images !== []){
            const headers = {
                'Content-Type': 'application/json;charset=UTF-8',
                'Accept' : '*/*',
                'Access-Control-Allow-Origin': '*'
              }
            const gameData = {
                "Name" : gameName,
                "AvgCompletionTime" : avgPlayTime,
                "AvgRating" : 0.0,
                "Description" : description,    
            }
            formData.append(gameData);
            const response = await axios.post(
                "https://localhost:7278/api/games/add", formData, { headers: headers }
            );
        }     
    }

    const HandleImagesUpload = (event) => {
        const formData = event.target.files;
        setImages(formData);
    }

    const HandleCoverUpload = (event) => {
        const formData = event.target.files;
        setCover(formData);
    }

    return <div id="addgameform">
        <form onSubmit={ HandleSubmit }>
            <label className="formLabel" htmlFor="name">Nazwa gry : </label>
            <input className="formInput" type="text" onChange={(e) => setGameName(e.target.value)}/>
            <br />
            <label className="formLabel" htmlFor="description">Opis : </label>
            <textarea className="formTextArea" rows="10" maxLength="400" onChange={(e) => setDescription(e.target.value)}/>
            <br />
            <label className="formLabel" htmlFor="email">Średni czas ukończenia : </label>
            <input className="formInput" type="number" onChange={(e) => setAvgPlayTime(e.target.value)}/>
            <label className="formLabel" htmlFor="screnshots">Screeny z gry:</label>
            <input type="file" multiple onChange={HandleImagesUpload}/>
            <label className="formLabel" htmlFor="screnshots">Screeny okładki:</label>
            <input type="file" onChange={HandleCoverUpload}/>
            <input type="submit" value="Dodaj grę" />
            <br />
        </form>
    </div>
    
}