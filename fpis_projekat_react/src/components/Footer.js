import { Component } from 'react';
import './Footer.css'

class Footer extends Component 
{    
    render() 
    {
        const currentYear = new Date().getFullYear();

        return (
            <p><a href=''>FPIS project | Sergej Aleksandar Gojković | {currentYear}</a></p>
        );
    }
}

export default Footer;