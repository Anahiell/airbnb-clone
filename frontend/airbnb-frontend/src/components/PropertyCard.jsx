import styles from "../styles/PropertyCard.module.css";

const PropertyCard = ({ image, title, location, price, rating }) => {
  return (
    <div className={styles.card}>
      <img src={image} alt={title} className={styles.image} />
      <div className={styles.info}>
        <h3>{title}</h3>
        <p>{location}</p>
        <p>{price} / ночь</p>
        <p>⭐ {rating}</p>
      </div>
    </div>
  );
};

export default PropertyCard;
