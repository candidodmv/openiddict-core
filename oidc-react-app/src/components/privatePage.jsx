import React, {useState} from 'react';
import AuthService from '../services/authService';


export const PrivatePage = () => {
    const [apiClientResponse, setApiClientResponse] = useState("Nothing yet")

    const clickHandler = async () => {
        const service = new AuthService();
        const user = await service.getUser();
        //console.log(user);

        if(user && user.access_token){
            fetch("https://localhost:44360/ApiContent/Private", {
                    mode: 'cors',
                    headers: {
                    'Authorization': `Bearer ${user.access_token}`,
                    'Access-Control-Allow-Origin': "*"
                }
            }).then(r => r.text()).then(t => setApiClientResponse(t));
        }
        else{
            console.error("ERR_GET_USER_STATE");
        }
    }

    return (
        <div>
            Private page
            <button onClick={clickHandler}>Request on Api.Client</button>
            <h2>{apiClientResponse}</h2>
        </div>
    );
};
