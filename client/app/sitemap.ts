import siteMetadata from "./utils/siteMetaData";
import { getFeed } from "./utils/api";

export default async function sitemap() {
  const response = await getFeed();
  const data = response.data.data;

  const posts = data.map(({ slug, createdAt }: { slug: string, createdAt: string } ) => ({
    url: `${siteMetadata.siteUrl}/post/${slug}`,
    lastModified: createdAt,
  }));

  const routes = ["", "/projects", "/post"].map((route) => ({
    url: `${siteMetadata.siteUrl}${route}`,
    lastModified: new Date().toISOString(),
  }));

  return [...routes, ...posts];
}
