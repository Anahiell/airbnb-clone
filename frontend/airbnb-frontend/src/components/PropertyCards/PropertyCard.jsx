import styles from "./PropertyCard.module.css";
import PropTypes from "prop-types";

const PropertyCard = ({ id, image, title, location, price, rating }) => {
  return (
    <div className={styles.card}>
      <img src={image} alt={title} className={styles.image} />
      <div className={styles.info}>
        <h3>{title}</h3>
        <p>{location}</p>
        <div className={styles.footer}>
          <span className={styles.price}>{price} / ночь</span>
          <span className={styles.rating}>⭐ {rating}</span>
        </div>
      </div>
    </div>
  );
};

// ✅ Добавляем валидацию пропсов
PropertyCard.propTypes = {
  id: PropTypes.number.isRequired,     // ID должно быть числом
  image: PropTypes.string.isRequired,  // URL изображения - строка
  title: PropTypes.string.isRequired,  // Название объекта
  location: PropTypes.string.isRequired, // Локация объекта
  price: PropTypes.string.isRequired,  // Цена - число
  rating: PropTypes.number.isRequired, // Рейтинг - число
};

export default PropertyCard;
