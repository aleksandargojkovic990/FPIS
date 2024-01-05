import { Component } from 'react';
import Gallery from './Gallery';
import './Home.css'

class Home extends Component 
{    
    handlePages = async () => 
    {
        this.props.handlePages(false);
    };

    render() 
    {
        const galleryImages = [
            { img: '/images/gallery/winery7.jpg' },
            { img: '/images/gallery/winery4.jpg' },
            { img: '/images/gallery/winery2.jpg' },
            { img: '/images/gallery/winery5.jpg' },
            { img: '/images/gallery/winery6.jpg' },
            { img: '/images/gallery/winery8.jpg' }
        ]

        return (
            <section id='home' className='row'>
                <h2 className='text-center my-5'>DOBRODOŠLI U FONDRUM VINA!</h2>

                <Gallery galleryImages={galleryImages} />

                <article id='wine-description-holder' className='col-8 col-sm-8 col-md-8 col-lg-8 col-xl-8 mx-auto mt-5 text-left'>
                    <p className='wine-description'>
                        FONdrum vina je vinarija osnovana još 1971. godine, od kada teži da sačuva tradiciju izrade najkvalitetnijeg vina
                        u regionu. Početkom 21. veka ova vinarija postaje sinonim za luksuz i vina postaju dostupna u skoro svim gradovima.
                        Ovako veliki uspeh je stečen pažljivim odabirom najboljeg grožđa na našim vinogradima, kako bi naši kupci dobili 
                        ekskluzivan proizvod vrhunskog kvaliteta.
                    </p>
                    <p className='wine-description mb-0'>
                        U našoj vinariji možete pronaći vina različitih sorti i stilova, koja su višestruki pobednici na različitim
                        takmičenjima i dobro prepoznatljiva po svom neodoljivom ukusu. Dugme FONdrum vina, u nastavku, Vam omogućava da 
                        uskočite u čarobni svet vina i sami se uverite zašto smo baš mi najbolji!
                    </p>
                </article>
                    
                <div className='p-2 col-12 text-center mt-5'>
                    <button id='btnFONdrum' className='btn px-4 py-2' onClick={this.handlePages}>FONdrum vina</button>
                </div>
                
            </section>
        );
    }
}

export default Home;