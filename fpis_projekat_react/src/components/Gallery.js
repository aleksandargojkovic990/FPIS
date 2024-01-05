import { useState } from 'react'
import './Gallery.css'

function Gallery ({galleryImages})
{
    const [slideNumber, setSlideNumber] = useState(0)
    const [openModal, setOpenModal] = useState(false)

    const handleOpenModal = (index) => {
        setSlideNumber(index)
        setOpenModal(true)
    }

    const handleCloseModal = () => {
        setOpenModal(false)
    }

    const handlePrevSlide = () => {
        setSlideNumber(slideNumber === 0 ? galleryImages.length - 1 : slideNumber - 1)
    }

    const handleNextSlide = () => {
        setSlideNumber(slideNumber === galleryImages.length - 1 ? 0 : slideNumber + 1)
    }

    return (
        <section id='gallery'>
            {
                openModal && 
                <div className='sliderWrap'>
                    <button className='btnClose text-dark' onClick={handleCloseModal}>x</button>
                    <button className='btnPrev text-dark' onClick={handlePrevSlide}>{"<"}</button>
                    <button className='btnNext text-dark' onClick={handleNextSlide}>{">"}</button>

                    <div className='fullScreenImage'>
                        <img src={galleryImages[slideNumber].img} alt='winery'></img>
                    </div>
                </div>
            }

            <div className='galleryWrap my-4'>
                {
                    galleryImages && galleryImages.map((slide, index) => {
                        return(
                            <div className='single' key={index} onClick={ () => handleOpenModal(index) }>
                                <img src={slide.img} alt='winery' />
                            </div>
                        )
                    })
                }
            </div>
        </section>
    )
}

export default Gallery