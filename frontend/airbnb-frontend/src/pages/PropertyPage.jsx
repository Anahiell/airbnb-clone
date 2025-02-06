import { useParams } from "react-router-dom";
import properties from "../data/properties";
import styles from "../styles/PropertyPage.module.css";

const PropertyPage = () => {
  const { id } = useParams();
  const property = properties.find((p) => p.id === Number(id));

  if (!property) {
    return <h2 className={styles.notFound}>Объект не найден 😞</h2>;
  }

  return (
    <div className={styles.propertyPage}>
      {/* Фотографии */}
      <div className={styles.imageGallery}>
        <img src={property.image} alt={property.title} className={styles.mainImage} />
      </div>

      {/* Информация */}
      <div className={styles.propertyInfo}>
        <h1>{property.title}</h1>
        <p className={styles.location}>{property.location}</p>
        <p className={styles.rating}>⭐ {property.rating} ({property.reviews} отзывов)</p>
        <p className={styles.description}>{property.description}</p>

        {/* Цена и кнопка бронирования */}
        <div className={styles.booking}>
          <span className={styles.price}>{property.price} / ночь</span>
          <button className={styles.bookButton}>Забронировать</button>
        </div>
      </div>
    </div>
  );
};

export default PropertyPage;
