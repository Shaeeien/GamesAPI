import React from "react";
import './index.css';

export function Content(data) {
    console.log(data);
    return <div className="content">                                
        {data.data.map(d => <div>{d}</div>)}
    </div>

}
