import axios from "axios";
import siteMetadata from "./utils/siteMetaData";

export default async function sitemap() {
  const response = await axios.get(process.env.NEXT_PUBLIC_API_URL + "/post");
  const data = response.data.data;

  const posts = data.items.map(({ slug, createdAt }: { slug: string, createdAt: string } ) => ({
    url: `${siteMetadata.siteUrl}/post/${slug}`,
    lastModified: createdAt,
  }));

  const routes = ["", "/projects"].map((route) => ({
    url: `${siteMetadata.siteUrl}${route}`,
    lastModified: new Date().toISOString(),
  }));

  return [...routes, ...posts];
}
