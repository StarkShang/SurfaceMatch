using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UFTypes
{
    class DummyType
    {
        public const int UF_dummy_type = 1;/* For internal use only */
    }
    class PointType
    {
        public const int UF_point_type = 2;
        public const int UF_point_subtype = 0;
        public const int UF_old_point_1_subtype = 1;/* Obsolete in v10 */
        public const int UF_old_point_2_subtype = 2;/* Obsolete in v10 */
    }
    class LineType
    {
        public const int UF_line_type = 3;
        public const int UF_line_normal_subtype = 0;
        public const int UF_line_old_subtype = 1;/* Obsolete in v10 */
        public const int UF_sketch_ref_line_subtype = 101;/* Obsolete in V17.0. */
        public const int UF_infinite_line_subtype = 2;
    }
    class SolideCollectionType
    {
        public const int UF_solid_collection_type = 4;/* Obsolete in V10 - Reused in NX 9.0 for UF_image_type */
    }
    class ImageType
    {
        public const int UF_image_type = 4;/* new in NX9.0 */
        public const int UF_image_raster_image_subtype = 0;/* new in NX9.0 */
    }
    class CircleType
    {
        public const int UF_circle_type = 5;/* Also called "arc" */
        public const int UF_circle_open_subtype = 0;/* Not Used - To determine whether a circle is open or closed please use UF_MODL_ask_curve_periodicity */
        public const int UF_circle_closed_subtype = 1;/* Not Used - To determine whether a circle is open or closed please use UF_MODL_ask_curve_periodicity */
        public const int UF_sketch_ref_circle_subtype = 101;/* Obsolete in V17.0. */
    }
    class ConicType
    {
        public const int UF_conic_type = 6;
        public const int UF_conic_ellipse_subtype = 2;
        public const int UF_conic_parabola_subtype = 3;
        public const int UF_conic_hyperbola_subtype = 4;
    }
    class SpcurveType
    {
        public const int UF_spcurve_type = 7;/* Obsolete in V10 */
        public const int UF_spcurve_open_subtype = 1;/* Obsolete in V10 */
        public const int UF_spcurve_closed_subtype = 2;/* Obsolete in V10 */
        public const int UF_spcurve_periodic_subtype = 3;/* Obsolete in V10 */
    }
    class OldSplineType
    {
        public const int UF_old_spline_type = 8;/* Obsolete in V10 - Reused in NX 9.0 for UF_product_interface_type */
        public const int UF_spline_open_subtype = 1;/* Obsolete in V10 - Reused in NX 9.0 for UF_product_interface_geometry_subtype */
        public const int UF_spline_closed_subtype = 2;/* Obsolete in V10 - Reused in NX 9.0 for UF_product_interface_collection_subtype */
    }
    class SplineType
    {
        public const int UF_spline_type = 9;/*  Renamed in V10 */
        public const int UF_spline_subtype = 0;
        public const int UF_b_curve_bezier_subtype = 0;/* There is no difference;
                                                             between subtype 0 and;
                                                             subtype 1 */
        public const int UF_b_curve_b_spline_subtype = 1;/* There is no difference;
                                                             between subtype 0 and;
                                                             subtype 1 */
        public const int UF_sketch_ref_spline_subtype = 101;/* Obsolete in V17.0 */

    }
    class LayerCategoryType
    {
        public const int UF_layer_category_type = 12;
    }
    class KanjiType
    {
        public const int UF_kanji_type = 13;
    }
    class BoundaryType
    {
        public const int UF_boundary_type = 14;
    }
    class GroupType
    {
        public const int UF_group_type = 15;
    }
    class PartAttributeType
    {
        public const int UF_part_attribute_type = 11;
        public const int UF_part_attribute_subtype = 0;
        public const int UF_part_attribute_cache_subtype = 1;
        public const int UF_temporary_part_attribute_subtype = 2;
    }
    class CylinderType
    {
        public const int UF_cylinder_type = 16;/* Obsolete in V10 */
        public const int UF_surface_normal_parallel_subtype = 0;
        public const int UF_surface_normal_flipped_subtype = 10;
    }
    class ConeType
    {
        public const int UF_cone_type = 17;/* Obsolete in V10 */
        public const int UF_surface_normal_parallel_subtype = 0;
        public const int UF_surface_normal_flipped_subtype = 10;
    }
    class SphereType
    {
        public const int UF_sphere_type = 18;/* Obsolete in V10 */
        public const int UF_surface_normal_parallel_subtype = 0;
        public const int UF_surface_normal_flipped_subtype = 10;
    }
    class SurfaceOfRevolutionType
    {
        public const int UF_surface_of_revolution_type = 19;/* Obsolete in V10 */
        public const int UF_surface_normal_parallel_u_subtype = 0;
        public const int UF_surface_normal_parallel_v_subtype = 1;
        public const int UF_surface_normal_flipped_u_subtype = 10;
        public const int UF_surface_normal_flipped_v_subtype = 11;
    }
    class TabulatedCylinderType
    {
        public const int UF_tabulated_cylinder_type = 20;/* Obsolete in V10 */
        public const int UF_surface_normal_parallel_subtype = 0;
        public const int UF_surface_normal_flipped_subtype = 10;
    }
    class RuledSurfaceType
    {
        public const int UF_ruled_surface_type = 21;/* Obsolete in V10 */
        public const int UF_surface_normal_parallel_subtype = 0;
        public const int UF_surface_normal_flipped_subtype = 10;
    }
    class BoundedPlaneType
    {
        public const int UF_bounded_plane_type = 22;/* Obsolete in V10 */
        public const int UF_surface_normal_parallel_subtype = 0;
        public const int UF_surface_normal_flipped_subtype = 10;
    }
    class BlendedFaceType
    {
        public const int UF_blended_face_type = 23;/* Obsolete in V10 */
        public const int UF_surface_normal_parallel_subtype = 0;
        public const int UF_surface_normal_flipped_subtype = 10;
    }
    class SculpturedSurfaceType
    {
        public const int UF_sculptured_surface_type = 24;/* Obsolete in V10 */
        public const int UF_surface_normal_parallel_subtype = 0;
        public const int UF_surface_normal_flipped_subtype = 10;
    }
    class BSurfaceType
    {
        public const int UF_b_surface_type = 43;
        public const int UF_b_surface_bezier_subtype = 0;/* Obsolete in V10 */
        public const int UF_b_surface_b_spline_subtype = 1;/* Obsolete in V10 */
    }
    class OffsetSurfaceType
    {
        public const int UF_offset_surface_type = 65;
        public const int UF_surface_normal_parallel_subtype = 0;
        public const int UF_surface_normal_flipped_subtype = 10;
    }
    class ForeignSurfaceType
    {
        public const int UF_foreign_surface_type = 66;/* Customer defined */
        public const int UF_surface_normal_parallel_subtype = 0;
        public const int UF_surface_normal_flipped_subtype = 10;
    }
    class SolidType
    {
        public const int UF_solid_type = 70;
        public const int UF_solid_body_subtype = 0;
        public const int UF_solid_swept_body_subtype = 1;/* Internal use only - not displayable */
        public const int UF_solid_face_subtype = 2;
        public const int UF_solid_edge_subtype = 3;
        public const int UF_solid_silhouette_subtype = 4;/* Moved to type 201 in V10 */
        public const int UF_solid_foreign_surf_subtype = 5;
    }
    class FaceType
    {
        public const int UF_face_type = 71;/* Obsolete in V10 */
        public const int UF_cylinder_subtype = 16;/* Obsolete in V10 */
        public const int UF_cone_subtype = 17;/* Obsolete in V10 */
        public const int UF_sphere_subtype = 18;/* Obsolete in V10 */
        public const int UF_surface_of_revolution_subtype = 19;/* Obsolete in V10 */
        public const int UF_tabulated_cylinder_subtype = 20;/* Obsolete in V10 */
        public const int UF_ruled_surface_subtype = 21;/* Obsolete in V10 */
        public const int UF_bounded_plane_subtype = 22;/* Obsolete in V10 */
        public const int UF_fillet_surface_subtype = 23;/* Obsolete in V10 */
        public const int UF_sculptured_surface_subtype = 24;/* Obsolete in V10 */
        public const int UF_b_surface_subtype = 43;/* Obsolete in V10 */
        public const int UF_offset_surface_subtype = 65;/* Obsolete in V10 */
        public const int UF_foreign_surface_subtype = 66;/* Obsolete in V10 */
    }
    class EdgeType
    {
        public const int UF_edge_type = 72;/* Obsolete in V10 */
        public const int UF_edge_0_subtype = 0;
        public const int UF_edge_3_subtype = 3;
    }
    class MachiningParameterSetType
    {
        public const int UF_machining_parameter_set_type = 107;
        public const int UF_mach_mill_post_cmnds_subtype = 11;
        public const int UF_mach_lathe_post_cmnds_subtype = 13;
        public const int UF_mach_wed_post_cmnds_subtype = 17;
        public const int UF_mach_pocket_subtype = 110;
        public const int UF_mach_surface_contour_subtype = 210;
        public const int UF_mach_vasc_subtype = 211;
        public const int UF_mach_gssm_main_op_subtype = 220;
        public const int UF_mach_gssm_sub_op_subtype = 221;
        public const int UF_mach_gssm_grip_subtype = 222;
        public const int UF_mach_param_line_subtype = 230;
        public const int UF_mach_zig_zag_surf_subtype = 240;
        public const int UF_mach_rough_to_depth_subtype = 250;
        public const int UF_mach_cavity_milling_subtype = 260;
        public const int UF_mach_lathe_rough_subtype = 310;
        public const int UF_mach_lathe_finish_subtype = 320;
        public const int UF_mach_lathe_groove_subtype = 330;
        public const int UF_mach_lathe_thread_subtype = 340;
        public const int UF_mach_drill_subtype = 350;
        public const int UF_mach_lathe_face_subtype = 360;
        public const int UF_mach_point_to_point_subtype = 450;
        public const int UF_mach_seq_curve_mill_subtype = 460;
        public const int UF_mach_seq_curve_lathe_subtype = 461;
        public const int UF_mach_wedm_subtype = 700;
        public const int UF_mach_mill_ud_subtype = 800;
        public const int UF_mach_mill_mc_subtype = 1100;
        public const int UF_mach_lathe_mc_subtype = 1200;
        public const int UF_mach_wedm_mc_subtype = 1300;
        public const int UF_mach_lathe_ud_subtype = 1400;
        public const int UF_mach_wedm_ud_subtype = 1500;
        public const int UF_mach_mass_edit_subtype = 1600;
    }
    class MachiningBoundaryMemberType
    {
        public const int UF_machining_boundary_member_type = 114;
        public const int UF_mach_geom_boundary_0_subtype = 0;
        public const int UF_mach_geom_boundary_subtype = 5;
        public const int UF_mach_geom_face_boundary_subtype = 7;
        public const int UF_mach_geom_camgeom_subtype = 9;
        public const int UF_mach_geom_camgeom_data_subtype = 10;
    }
    class MachiningMasterOperationType
    {
        public const int UF_machining_master_operation_type = 115;
        public const int UF_mach_wedm_external_trim_subtype = 0;
        public const int UF_mach_wedm_internal_trim_subtype = 1;
        public const int UF_mach_wedm_no_core_subtype = 2;
        public const int UF_mach_wedm_open_profile_subtype = 3;
        public const int UF_mach_wedm_cutoff_subtype = 4;
        public const int UF_mach_wedm_rough_pass_subtype = 5;
        public const int UF_mach_wedm_backburn_subtype = 6;
        public const int UF_mach_wedm_finish_trim_subtype = 7;
        public const int UF_mach_wedm_ext_finish_trim_subtype = 8;
        public const int UF_mach_wedm_subtype = 700;
    }
    class MachiningPostCommandType
    {
        public const int UF_machining_post_command_type = 116;
        public const int UF_machining_mce_startup_subtype = 1;
        public const int UF_machining_mce_endofpath_subtype = 2;
        public const int UF_machining_mce_inpath_subtype = 3;
        public const int UF_machining_mce_wedm_startup_subtype = 4;
        public const int UF_machining_mce_wedm_endofpath_subtype = 5;
        public const int UF_machining_mce_wedm_inpath_subtype = 6;
        public const int UF_machining_mce_mill_mc_subtype = 7;
        public const int UF_machining_mce_lathe_mc_subtype = 8;
        public const int UF_machining_mce_wedm_mc_subtype = 9;
        public const int UF_machining_mce_number_subtype = 10;
    }
    class MachiningSuboperation
    {
        public const int UF_machining_suboperation = 117;
        public const int UF_mach_subop_ncm_subtype = 20;
        public const int UF_mach_subop_ncm_engret_subtype = 21;
        public const int UF_mach_subop_ncm_appdep_subtype = 22;
        public const int UF_mach_subop_ncm_trav_subtype = 23;
        public const int UF_mach_subop_region_subtype = 30;
        public const int UF_mach_subop_region_shape_subtype = 31;
        public const int UF_mach_subop_region_element_subtype = 32;
        public const int UF_mach_subop_blade_subtype = 40;
        public const int UF_mach_subop_containment_subtype = 41;
        public const int UF_mach_subop_floorwall_subtype = 42;
        public const int UF_mach_subop_tool_axis_data_subtype = 43;
        public const int UF_mach_subop_command_subtype = 44;
        public const int UF_tilt_subop_data_subtype = 45;
        public const int UF_mach_region_manager_subtype = 46;

        public const int UF_mach_manual_move_subtype = 100;
        public const int UF_mach_manual_move_data_subtype = 101;
        public const int UF_insp_move_subtype = 200;
        public const int UF_insp_move_data_subtype = 201;
        public const int UF_mach_laser_move_subtype = 300;
        public const int UF_mach_laser_move_data_subtype = 301;
    }
    class MachiningBoundaryType
    {
        public const int UF_machining_boundary_type = 118;
        public const int UF_mach_geom_boundary_subtype = 5;
        public const int UF_mach_geom_camgeom_subtype = 9;
    }
    class MachiningControlEventType
    {
        public const int UF_machining_control_event_type = 119;
        public const int UF_cevent_motion_subtype = 100;
        public const int UF_cevent_end_of_path_subtype = 101;
        public const int UF_cevent_start_of_path_subtype = 102;
        public const int UF_cevent_start_point_output_subtype = 103;
        public const int UF_cevent_mom_post_event_subtype = 104;
        public const int UF_cevent_3x_linear_subtype = 150;
        public const int UF_cevent_3x_linear_with_feed_subtype = 151;
        public const int UF_cevent_3x_linear_cust_feed_subtype = 152;
        public const int UF_cevent_5x_linear_subtype = 153;
        public const int UF_cevent_5x_linear_with_feed_subtype = 154;
        public const int UF_cevent_5x_linear_cust_feed_subtype = 155;
        public const int UF_cevent_3x_circular_subtype = 156;
        public const int UF_cevent_3x_circular_with_feed_subtype = 157;
        public const int UF_cevent_3x_circular_cust_feed_subtype = 158;
        public const int UF_cevent_5x_circular_subtype = 159;
        public const int UF_cevent_5x_circular_with_feed_subtype = 160;
        public const int UF_cevent_5x_circular_cust_feed_subtype = 161;
        public const int UF_cevent_3x_helical_subtype = 162;
        public const int UF_cevent_3x_helical_with_feed_subtype = 163;
        public const int UF_cevent_3x_helical_cust_feed_subtype = 164;
        public const int UF_cevent_5x_helical_subtype = 165;
        public const int UF_cevent_5x_helical_with_feed_subtype = 166;
        public const int UF_cevent_5x_helical_cust_feed_subtype = 167;
        public const int UF_cevent_3x_nurbs_subtype = 168;
        public const int UF_cevent_3x_nurbs_with_feed_subtype = 169;
        public const int UF_cevent_3x_nurbs_cust_feed_subtype = 170;
        public const int UF_cevent_5x_nurbs_subtype = 171;
        public const int UF_cevent_5x_nurbs_with_feed_subtype = 172;
        public const int UF_cevent_5x_nurbs_cust_feed_subtype = 173;
        public const int UF_cevent_mce_fromPoint_subtype = 200;
        public const int UF_cevent_mce_startPoint_subtype = 201;
        public const int UF_cevent_mce_startEngage_subtype = 202;
        public const int UF_cevent_mce_returnPoint_subtype = 203;
        public const int UF_cevent_mce_gohomePoint_subtype = 204;
        public const int UF_cevent_mce_toolChange_subtype = 205;
        public const int UF_cevent_mce_origin_subtype = 206;
        public const int UF_cevent_mce_seqno_subtype = 207;
        public const int UF_cevent_mce_setModes_subtype = 208;
        public const int UF_cevent_mce_selectHead_subtype = 209;
        public const int UF_cevent_mce_cutcom_subtype = 210;
        public const int UF_cevent_mce_spindleOn_subtype = 211;
        public const int UF_cevent_mce_spindleOff_subtype = 212;
        public const int UF_cevent_mce_coolantOn_subtype = 213;
        public const int UF_cevent_mce_coolantOff_subtype = 214;
        public const int UF_cevent_mce_optStop_subtype = 215;
        public const int UF_cevent_mce_stop_subtype = 216;
        public const int UF_cevent_mce_optSkipOn_subtype = 217;
        public const int UF_cevent_mce_optSkipOff_subtype = 218;
        public const int UF_cevent_mce_dwell_subtype = 219;
        public const int UF_cevent_mce_cycle_subtype = 220;
        public const int UF_cevent_mce_auxfun_subtype = 221;
        public const int UF_cevent_mce_prefun_subtype = 222;
        public const int UF_cevent_mce_clamp_subtype = 223;
        public const int UF_cevent_mce_toolLengthComp_subtype = 224;
        public const int UF_cevent_mce_rotate_subtype = 225;
        public const int UF_cevent_mce_toolPreselect_subtype = 226;
        public const int UF_cevent_mce_userDefined_subtype = 227;
        public const int UF_cevent_mce_pprint_subtype = 228;
        public const int UF_cevent_mce_opMessage_subtype = 229;
        public const int UF_cevent_mce_goto_subtype = 230;
        public const int UF_cevent_mce_threadWire_subtype = 231;
        public const int UF_cevent_mce_cutWire_subtype = 232;
        public const int UF_cevent_mce_flush_subtype = 233;
        public const int UF_cevent_mce_flushTank_subtype = 234;
        public const int UF_cevent_mce_power_subtype = 235;
        public const int UF_cevent_mce_wireGuides_subtype = 236;
        public const int UF_cevent_mce_wireAngle_subtype = 237;
        public const int UF_cevent_mce_fedrat_subtype = 238;
        public const int UF_cevent_mce_wireCutcom_subtype = 239;
        public const int UF_cevent_mce_latheThread_subtype = 240;
        public const int UF_cevent_mce_goDelta_subtype = 241;
        public const int UF_cevent_mce_from_subtype = 242;
        public const int UF_cevent_mce_goHome_subtype = 243;
        public const int UF_cevent_ude_subtype = 244;
        public const int UF_cevent_ud_path_subtype = 245;
        public const int UF_cevent_start_of_pass_subtype = 246;
        public const int UF_cevent_end_of_pass_subtype = 247;
        public const int UF_cevent_mce_smoothLeadIn_subtype = 248;
        public const int UF_cevent_mce_smoothLeadOut_subtype = 249;
        public const int UF_cevent_mce_spindleReverse_subtype = 250;
        public const int UF_cevent_mce_trackingPointChange_subtype = 251;
        public const int UF_cevent_mf_message_subtype = 400;
        public const int UF_cevent_mf_close_debug_files_subtype = 401;
        public const int UF_cevent_mf_dump_buffers_subtype = 402;
        public const int UF_cevent_mf_change_status_subtype = 403;
        public const int UF_cevent_mf_last_clsf_event_subtype = 404;
        public const int UF_cevent_mf_start_of_fillet_subtype = 405;
        public const int UF_cevent_mf_output_gohome_subtype = 406;
        public const int UF_cevent_mf_highlight_subtype = 407;
        public const int UF_cevent_mf_fillet_params_subtype = 408;
        public const int UF_cevent_mf_operation_name_subtype = 409;
        public const int UF_cevent_mf_tldata_subtype = 410;
        public const int UF_cevent_mf_msys_subtype = 411;
        public const int UF_cevent_mf_list_deletion_subtype = 413;
        public const int UF_cevent_mf_local_return_start_subtype = 414;
        public const int UF_cevent_mf_local_return_end_subtype = 415;
        public const int UF_cevent_mf_display_font_subtype = 416;
        public const int UF_cevent_mf_slowdown_params_subtype = 417;
        public const int UF_cevent_mf_cut_level_plane_subtype = 418;
        public const int UF_cevent_mf_counter_value_subtype = 419;
        public const int UF_cevent_mf_gouge_subtype = 420;
        public const int UF_cevent_mf_unpropagable_event_subtype = 421;
        public const int UF_cevent_scud_updown_cut_subtype = 600;
        public const int UF_cevent_set_marker_subtype = 601;
        public const int UF_cevent_manual_pattern_action_subtype = 602;
        public const int UF_cevent_udc_subtype = 603;
        public const int UF_cevent_udc_off_subtype = 604;
        public const int UF_mach_sync_event_subtype = 700;
    }
    class MachiningNcmType
    {
        public const int UF_machining_ncm_type = 120;
        public const int UF_mach_ncm_subtype = 10;
        public const int UF_mach_ncm_point_subtype = 20;
        public const int UF_mach_ncm_engret_subtype = 30;
        public const int UF_mach_ncm_transfer_subtype = 40;
        public const int UF_mach_ncm_clgeom_subtype = 50;
    }
}
