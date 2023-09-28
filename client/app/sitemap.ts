import axios from "axios";
import siteMetadata from "./utils/siteMetaData";

export default async function sitemap() {
  const response = await axios.get("http://localhost:5000/api/post");
  const data = response.data.data;

  const posts = data.map(({ slug, createdAt }: { slug: string, createdAt: string } ) => ({
    url: `${siteMetadata.siteUrl}/${slug}`,
    lastModified: createdAt,
  }));

  const routes = ["", "/portfolio", "/blog"].map((route) => ({
    url: `${siteMetadata.siteUrl}${route}`,
    lastModified: new Date().toISOString(),
  }));

  return [...routes, ...posts];
}
