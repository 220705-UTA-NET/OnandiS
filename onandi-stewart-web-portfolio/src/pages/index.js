import * as React from "react"
import { graphql } from "gatsby"
import Layout from "../components/layout"
import * as sections from "../components/sections"
import Fallback from "../components/fallback"
import { Canvas } from "@react-three/fiber";
import { OrbitControls } from "@react-three/drei";
import Background from "../components/Background";
import Box from "../components/Box";
import AnimatedSphere from "../components/AnimatedSphere";
import { StrictMode } from "react";
import ReactDOM from "react-dom";
import App from "../pages/App";


export default function Homepage(props) {
  const { homepage } = props.data;


  return (
    <Layout {...homepage}>
       
      {homepage.blocks.map((block) => {
        const { id, blocktype, ...componentProps } = block
        const Component = sections[blocktype] || Fallback
        return <Component key={id} {...componentProps} />
      })}
     <App />
    </Layout>
  )
}

export const query = graphql`
  {
    homepage {
      id
      title
      description
      image {
        id
        url
      }
      blocks: content {
        id
        blocktype
        ...HomepageHeroContent
        ...HomepageFeatureListContent
        ...HomepageCtaContent
        ...HomepageLogoListContent
        ...HomepageTestimonialListContent
        ...HomepageBenefitListContent
        ...HomepageStatListContent
        ...HomepageProductListContent
      }
    }
  }
`
