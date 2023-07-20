export default function Post() {
  return (
    <div className="divPost">
      <div>
        <img src="https://picsum.photos/1920/1080" alt="hero" />
      </div>
      <div className="divTexts">
        <h2>Lorem ipsum dolor sit.</h2>
        <p className="pInfo">
          <a className="linkAuthor">Rian Negreiros</a>
          <time>2023-07-20 12:00</time>
        </p>
        <p className="pSummary">Lorem, ipsum dolor sit amet consectetur adipisicing elit. Magnam numquam mollitia sed praesentium, consequuntur enim libero, eum iure quis officiis ducimus, inventore consequatur voluptatibus. Accusamus explicabo voluptas animi minus officia.</p>
      </div>
    </div>
  )
}